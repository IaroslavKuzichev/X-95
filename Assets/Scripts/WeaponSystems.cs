using Bhaptics.SDK2;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WeaponSystems: MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _torpedoPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate;

    [SerializeField] private TextMeshProUGUI _torpedoText;
    [SerializeField] private TextMeshProUGUI _laserText;
    
    [SerializeField] private AudioSource _laserSource;
    [SerializeField] private AudioSource _torpedoSource;

    private bool _isOverheated;
    private int _torpedoCount;
    private float _fireDelay;
    private float _overheat;
    private float Overheat
    {
        get => _overheat;
        set
        {
            if (value > 100)
            {
                _overheat = 100;
            }
            else if (value < 0)
            {
                _overheat = 0;
            }
            else
            {
                _overheat = value;
            }
        }
    }
    private void Start()
    {
        if (gameObject.scene.name.StartsWith("L"))
        {
            Movement.input.Controller.Torpedo.performed += ctx => Torpedo();
            _fireDelay = 1 / _fireRate;
            _torpedoCount = 3;
            _torpedoText.text = $"Торпеды: {_torpedoCount}";
            Overheat = 0;
            _isOverheated = false;
        }
    }
    private void Update()
    {
        if (gameObject.scene.name.StartsWith("L"))
        {
            if (Movement.input.Controller.Laser.IsPressed())
            {
                if (_fireDelay <= 0f)
                {
                    StartCoroutine(Fire(_laserPrefab));
                    BhapticsLibrary.Play("laser_shot");
                    _laserSource.Play();
                    _fireDelay = 1 / _fireRate;
                }
                else
                {
                    _fireDelay -= Time.deltaTime;
                }
            }
            else
            {
                Overheat -= 10f * Time.deltaTime;
                _fireDelay = 0f;
            }
            if (!_isOverheated)
            {
                _laserText.text = $"Нагрев: {Math.Round(Overheat)}%";
            }
        }
    }
    private void Torpedo()
    {
        if (_torpedoCount > 0)
        {
            _torpedoSource.Play();
            StartCoroutine(Fire(_torpedoPrefab));
            Movement.rb.AddForce(-_firePoint.up * 500f, ForceMode.Force);
            BhapticsLibrary.Play("torpedo_shot");
            StartCoroutine(TorpedoWait());
            _torpedoCount--;
            _torpedoText.text = $"Торпеды: {_torpedoCount}";
        }
    }
    private IEnumerator Fire(GameObject proj)
    {
        if (Overheat < 100f)
        {
            GameObject Projectile = Instantiate(proj, _firePoint.position, _firePoint.rotation);
            Rigidbody rb = Projectile.GetComponent<Rigidbody>();
            rb.AddForce(_firePoint.up * 750f, ForceMode.Force);
            Overheat += 2;
            yield return new WaitForSeconds(3);
            Destroy(Projectile);
        }
        else
        {
            Movement.input.Controller.Laser.Disable();
            _laserText.text = "Перегрев орудия";
            _isOverheated = true;
            yield return new WaitForSeconds(5);
            _isOverheated = false;
            _laserText.text = "Нагрев: 0%";
            Overheat = 0;
            Movement.input.Controller.Laser.Enable();
        }
    }
    private IEnumerator TorpedoWait()
    {
        Movement.input.Controller.Torpedo.Disable();
        yield return new WaitForSeconds(3);
        Movement.input.Controller.Torpedo.Enable();
    }
}
