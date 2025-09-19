using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Столкновение с астероидом");
        Destroy(gameObject);
    }
}
