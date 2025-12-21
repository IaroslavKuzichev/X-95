using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Canvas _pauseScreen;
    [SerializeField] private Canvas _hudScreen;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private AudioSource _uiSource;

    private EventSystem _eventSystem;
    private void Awake()
    {
        _continueButton.onClick.AddListener(Continue);
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }
    private void Start()
    {
        if (gameObject.scene.name.StartsWith("L"))
        {
            _hudScreen.enabled = true;
            Movement.input.UI.Pause.performed += ctx => TogglePause();
            _eventSystem = FindAnyObjectByType<EventSystem>();
            _eventSystem.enabled = false;
        }
    }
    private void TogglePause()
    {
        _continueButton.Select();
        _pauseScreen.enabled = !_pauseScreen.enabled;
        _hudScreen.enabled = !_hudScreen.enabled;
        _eventSystem.enabled = !_eventSystem.enabled;
        if (Time.timeScale == 0)
        {
            Continue();
        }
        else
        {
            _uiSource.Play();
            Time.timeScale = 0;
            Movement.input.Controller.Disable();
        }
    }
    private void Continue()
    {
        _uiSource.Play();
        _pauseScreen.enabled = false;
        _eventSystem.enabled = false;
        Time.timeScale = 1;
        Movement.input.Controller.Enable();
    }
    private void Exit()
    {
        _uiSource.Play();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    private void Restart()
    {
        _uiSource.Play();
        Movement.input.Controller.Enable();
        Time.timeScale = 1;
        SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Single);
    }
}
