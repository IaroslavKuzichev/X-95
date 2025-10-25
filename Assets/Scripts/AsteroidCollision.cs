using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private int _collisionCounter = 0;
    private void OnCollisionEnter(Collision collision)
    {
        _collisionCounter++;
        if (collision.gameObject.CompareTag("Player") && _collisionCounter < 5)
        {
            PlayerBehavior.PlayerHP -= 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
