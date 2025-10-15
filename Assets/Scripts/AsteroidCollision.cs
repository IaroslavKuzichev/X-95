using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBehavior.PlayerHP -= 2;
        }
    }
}
