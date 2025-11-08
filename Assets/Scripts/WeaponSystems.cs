using Bhaptics.SDK2;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystems: MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _torpedoPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate;

    [SerializeField] private TextMeshProUGUI _torpedoText;
    [SerializeField] private TextMeshProUGUI _laserText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _fireModeText;

    [SerializeField] private AudioSource _laserSource;
    [SerializeField] private AudioSource _torpedoSource;

    private float _fireDelay;
    private int _torpedoCount;
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
        Movement.input.Controller.Torpedo.performed += ctx => Torpedo();
        Movement.input.Controller.SwitchFire.performed += ctx => SwitchFire();
        _fireDelay = 1 / _fireRate;
        _torpedoCount = 5;
        _torpedoText.text = $"Кол-во торпед: {_torpedoCount}";
        Overheat = 0;
    }
    private void Update()
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
            BhapticsLibrary.StopByEventId("laser_shot");
        }
        _laserText.text = $"Перегрев: {Math.Round(Overheat)}";
        _healthText.text = $"Прочность обшивки: {PlayerBehavior.PlayerHP}";
    }
    private void Torpedo()
    {
        if (_torpedoCount > 0)
        {
            _torpedoSource.Play();
            StartCoroutine(Fire(_torpedoPrefab));
            BhapticsLibrary.Play("torpedo_shot");
            StartCoroutine(TorpedoWait());
            _torpedoCount--;
            _torpedoText.text = $"Кол-во торпед: {_torpedoCount}";
        }
    }
    private void SwitchFire()
    {
        if (_fireRate == 0)
        {
            _fireRate = 10;
            _fireModeText.text = "Автоматическая стрельба";
        }
        else
        {
            _fireRate = 0;
            _fireModeText.text = "Одиночная стрельба";
        }
    }
    private IEnumerator Fire(GameObject proj)
    {
        if (Overheat < 100f)
        {
            GameObject Projectile = Instantiate(proj, _firePoint.position, _firePoint.rotation);
            Rigidbody rb = Projectile.GetComponent<Rigidbody>();
            rb.AddForce(_firePoint.up * 1000f, ForceMode.Force);
            Overheat++;
            yield return new WaitForSecondsRealtime(3);
            Destroy(Projectile);
        }
        else
        {
            Movement.input.Controller.Laser.Disable();
            Debug.Log("Weapon overheat");
            yield return new WaitForSecondsRealtime(5);
            Movement.input.Controller.Laser.Enable();
        }
    }
    private IEnumerator TorpedoWait()
    {
        Movement.input.Controller.Torpedo.Disable();
        Movement.rb.AddForce(-_firePoint.up * 500f, ForceMode.Force);
        yield return new WaitForSecondsRealtime(3);
        Movement.input.Controller.Torpedo.Enable();
    }
}
