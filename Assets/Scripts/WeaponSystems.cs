using System.Collections;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private Transform _leftLaserPoint;
    [SerializeField] private Transform _rightLaserPoint;
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
        StartCoroutine(Fire(_laserPrefab, _leftLaserPoint));
    }
    private void FireRight()
    {
        StartCoroutine(Fire(_laserPrefab, _rightLaserPoint));
    }
    private void Torpedo()
    {
        _input.Controller.Torpedo.Disable();
        StartCoroutine(Fire(_torpedoPrefab, _torpedoPoint));
        _input.Controller.Torpedo.Enable();
    }
    private IEnumerator Fire(GameObject proj, Transform firePoint)
    {
        GameObject Projectile = Instantiate(proj, firePoint.position, firePoint.rotation);
        Rigidbody rb = Projectile.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * 500f, ForceMode.Force);
        yield return new WaitForSecondsRealtime(2);
        Destroy(Projectile);
    }
}
