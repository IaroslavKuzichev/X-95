using UnityEngine;

public class ShockwaveBehavior : MonoBehaviour
{
    [SerializeField] private SphereCollider _wave;
    private void Update()
    {
        if (_wave.radius < 5f)
        {
            _wave.radius += 0.05f;
            Blast();
        }
    }
    private void Blast()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, _wave.radius);
        foreach (Collider collider in hitObjects)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                rb.AddForce(direction * 0.03f, ForceMode.Impulse);
            }
        }
    }
}
