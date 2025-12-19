using Bhaptics.SDK2;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    private int _asteroidHP;
    public int AsteroidHP
    {
        get => _asteroidHP;
        set
        {
            if (value < 0) _asteroidHP = 0;
            else if (value > 100) _asteroidHP = 100;
            else _asteroidHP = value;
        }
    }
    private void Start()
    {
        AsteroidHP = 100;
    }
    private void OnCollisionEnter(Collision collision)
    {
        AsteroidHP -= 20;
        if (collision.gameObject.CompareTag("Player"))
        {
            BhapticsLibrary.Play("asteroid_collision");
            PlayerBehavior.PlayerHP -= 5;
        }
    }
    private void Update()
    {
        if (AsteroidHP <= 0)
        {
            BhapticsLibrary.StopByEventId("asteroid_collision");
            Destroy(gameObject);
        }
    }
}
