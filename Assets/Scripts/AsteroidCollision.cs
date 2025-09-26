using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehavior.PlayerHP -= 1;
        Debug.Log(PlayerBehavior.PlayerHP);
        Debug.Log("Столкновение с астероидом");
        Destroy(gameObject);
    }
}
