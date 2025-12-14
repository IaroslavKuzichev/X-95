using Bhaptics.SDK2;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private int _collisionCounter = 0;
    private void OnCollisionEnter(Collision collision)
    {
        _collisionCounter++;
        if (_collisionCounter < 5)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                BhapticsLibrary.Play("asteroid_collision");
                PlayerBehavior.PlayerHP -= 1;
            }
        }
        else
        {
            BhapticsLibrary.StopByEventId("asteroid_collision");
            Destroy(gameObject);
        }
    }
}
