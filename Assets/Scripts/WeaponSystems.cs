using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystems: MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _torpedoPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate;
    private CustomInput _input;
    private float _fireDelay;
    private int _torpedoCount;
    private int _overheat;
    private void Awake()
    {
        _input = new CustomInput();
        _input.Controller.Torpedo.performed += ctx => Torpedo();
        _input.Controller.SwitchFire.performed += ctx => SwitchFire();
        _fireDelay = 1 / _fireRate;
        _torpedoCount = 5;
    }
    private void Update()
    {
        if (_input.Controller.Laser.IsPressed())
        {
            if (_fireDelay <= 0f)
            {
                StartCoroutine(Fire(_laserPrefab));
                _fireDelay = 1 / _fireRate;
            }
            else
            {
                _fireDelay -= Time.deltaTime;
            }
            Gamepad.current.SetMotorSpeeds(0.75f, 0f);
        }
        else
        {
            _fireDelay = 0f;
            InputSystem.ResetHaptics();
        }
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
    private void Torpedo()
    {
        if (_torpedoCount > 0)
        {
            StartCoroutine(Fire(_torpedoPrefab));
            _torpedoCount--;
        }
    }
    private void SwitchFire()
    {
        if (_fireRate == 0)
        {
            _fireRate = 10;
        }
        else
        {
            _fireRate = 0;
        }
        Debug.Log($"Fire rate: {_fireRate}");
    }
    private IEnumerator Fire(GameObject proj)
    {
        if (_overheat < 100)
        {
            GameObject Projectile = Instantiate(proj, _firePoint.position, _firePoint.rotation);
            Rigidbody rb = Projectile.GetComponent<Rigidbody>();
            rb.AddForce(_firePoint.forward * 1000f, ForceMode.Force);
            _overheat++;
            yield return new WaitForSecondsRealtime(2);
            Destroy(Projectile);
        }
        else
        {
            _input.Controller.Laser.Disable();
            Debug.Log("Weapon overheat");
            yield return new WaitForSecondsRealtime(5);
            _input.Controller.Laser.Enable();
            _overheat = 0;
        }
    }
}
