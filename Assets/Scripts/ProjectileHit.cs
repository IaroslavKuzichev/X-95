using System.Collections;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _shockwavePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            if (gameObject.CompareTag("Torpedo"))
            {
                StartCoroutine(Explosion(other.gameObject.transform));
            }
            else
            {
                other.gameObject.GetComponent<AsteroidBehavior>().AsteroidHP -= 20;
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator Explosion(Transform parent)
    {
        GameObject explosion = Instantiate(_explosionPrefab, parent.position, parent.rotation);
        GameObject shockwave = Instantiate(_shockwavePrefab, parent.position, parent.rotation);
        Destroy(parent.gameObject); 
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<Collider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        Destroy(shockwave);
        Destroy(explosion);
        Destroy(gameObject);
    }
}
