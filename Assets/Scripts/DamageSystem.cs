using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("PlayerFront"))
        {
            StartCoroutine(WeaponDamage(target));
        }
        else if (target.CompareTag("PlayerBack"))
        {
            StartCoroutine(EngineDamage(target));
        }
    }
    private IEnumerator WeaponDamage(GameObject obj)
    {
        obj.GetComponentInParent<WeaponSystems>().enabled = false;
        Movement.input.Controller.Torpedo.Disable();
        PlayerBehavior.WeaponsDamaged = true;
        yield return new WaitForSeconds(5f);
        PlayerBehavior.WeaponsDamaged = false;
        Movement.input.Controller.Torpedo.Enable();
        obj.GetComponentInParent<WeaponSystems>().enabled = true;
    }
    private IEnumerator EngineDamage(GameObject obj)
    {
        obj.GetComponentInParent<Movement>().PlayerSpeed = 1f;
        PlayerBehavior.EnginesDamaged = true;
        yield return new WaitForSeconds(5f);
        PlayerBehavior.EnginesDamaged = false;
        obj.GetComponentInParent<Movement>().PlayerSpeed = 10;
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        GameObject obj = FindAnyObjectByType<XROrigin>().gameObject;
        obj.GetComponent<Movement>().PlayerSpeed = 10;
        Movement.input.Controller.Torpedo.Enable();
        obj.GetComponentInParent<WeaponSystems>().enabled = true;
        PlayerBehavior.WeaponsDamaged = false;
        PlayerBehavior.EnginesDamaged = false;
    }
}
