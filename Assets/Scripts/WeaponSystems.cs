using System.Collections;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private Transform _leftFirePoint;
    [SerializeField] private Transform _rightFirePoint;
    [SerializeField] private Transform _torpedoPoint;
    [SerializeField] private GameObject _torpedoPrefab;
    [SerializeField] private GameObject _laserPrefab;
    private CustomInput _input;
    private void Awake()
    {
        _input = new CustomInput();
        _input.Controller.FireLeftCannon.performed += ctx => FireLeft();
        _input.Controller.FireRightCannon.performed += ctx => FireRight();
        _input.Controller.Torpedo.performed += ctx => Torpedo();
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
    private void FireLeft()
    {
        StartCoroutine(FireLaser(_leftFirePoint));
    }
    private void FireRight()
    {
        StartCoroutine(FireLaser(_rightFirePoint));
    }
    private void Torpedo()
    {
        StartCoroutine(FireTorpedo());
    }
    private IEnumerator FireLaser(Transform firePoint)
    {
        GameObject Laser = Instantiate(_laserPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = Laser.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.up * 500f, ForceMode.Force);
        yield return new WaitForSecondsRealtime(1);
        Destroy(Laser);
    }
    private IEnumerator FireTorpedo()
    {
        GameObject Torpedo = Instantiate(_torpedoPrefab, _torpedoPoint.position, _torpedoPoint.rotation);
        Rigidbody rb = Torpedo.GetComponent<Rigidbody>();
        rb.AddForce(_torpedoPoint.forward * 500f, ForceMode.Force);
        _input.Controller.Torpedo.Disable();
        yield return new WaitForSecondsRealtime(2);
        Destroy(Torpedo);
        _input.Controller.Torpedo.Enable();
    }
}
