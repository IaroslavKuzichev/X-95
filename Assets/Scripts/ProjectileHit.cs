using System.Collections;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Explosion(other.gameObject.transform));
            Destroy(other.gameObject);
        }
    }
    private IEnumerator Explosion(Transform parent)
    {
        Debug.Log("Start");
        GameObject explosion = Instantiate(_explosionPrefab, parent.position, parent.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<Collider>().enabled = false;
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(explosion);
        Debug.Log("Finish");
        Destroy(gameObject);
    }
}
