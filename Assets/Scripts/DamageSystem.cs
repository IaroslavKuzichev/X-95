using System.Collections;
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
        PlayerBehavior.WeaponsDamaged = true;
        yield return new WaitForSeconds(5f);
        PlayerBehavior.WeaponsDamaged = false;
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
}
