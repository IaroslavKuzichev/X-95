using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerBehavior.CollectibleCount++;
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
