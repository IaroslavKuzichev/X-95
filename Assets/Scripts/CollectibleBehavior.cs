using TMPro;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _crystalCountText;
    [SerializeField] private float _rotationSpeed;

    private void Awake()
    {
        _crystalCountText.text = $"Кристаллы: {PlayerBehavior.CollectibleCount}";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFront") || other.gameObject.CompareTag("PlayerBack"))
        {
            PlayerBehavior.CollectibleCount++;
            _crystalCountText.text = $"Кристаллы: {PlayerBehavior.CollectibleCount}";
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
