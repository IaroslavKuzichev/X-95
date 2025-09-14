using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private int _playerHP;
    public int PlayerHP
    {
        get => _playerHP;
        set
        {
            if (value < 0) _playerHP = 0;
            else if (value > 100) _playerHP = 100;
            else _playerHP = value;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Негр");
    }
}
