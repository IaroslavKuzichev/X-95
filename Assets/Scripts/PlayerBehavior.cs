using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private Canvas _pauseScreen;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
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
        Movement.input.UI.Pause.performed += ctx => TogglePause();
        _continueButton.onClick.AddListener(Continue);
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }
    private void TogglePause()
    {
        _pauseScreen.enabled = !_pauseScreen.enabled;
        if (Time.timeScale == 0)
        {
            Continue();
        }
        else 
        {
            Time.timeScale = 0;
            Movement.input.Controller.Disable();
        }
    }
    private void Continue()
    {
        _pauseScreen.enabled = false;
        Time.timeScale = 1;
        Movement.input.Controller.Enable();
    }
    private void Exit()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
    private void Restart()
    {
        Debug.Log("Restart");
    }
}
