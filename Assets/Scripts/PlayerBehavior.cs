using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private Canvas PauseScreen;
    static private int _playerHP;
    static public int PlayerHP
    {
        get => _playerHP;
        set
        {
            if (value < 0) _playerHP = 0;
            else if (value > 100) _playerHP = 100;
            else _playerHP = value;
        }
    }
    private void Start()
    {
        PlayerHP = 100;
        Movement.input.Controller.Pause.performed += ctx => TogglePause();
    }
    private void TogglePause()
    {
        PauseScreen.enabled = !PauseScreen.enabled;
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else 
        {
            Time.timeScale = 0;
        }
    }
}
