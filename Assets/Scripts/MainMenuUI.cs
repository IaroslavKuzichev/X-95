using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _adjustButton;
    [SerializeField] private Button _settingsButton;

    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _settingsExitButton;
    [SerializeField] private Button _adjustExitButton;

    [SerializeField] private Canvas _settingsScreen;
    [SerializeField] private Canvas _adjustScreen;
    [SerializeField] private Canvas _menuScreen;

    [SerializeField] private Slider _sfxSlider;

    [SerializeField] private GameObject _camera;

    [SerializeField] private AudioSource _uiSource;

    private void Awake()
    {
        StartCoroutine(LoadDelay());
        _newGameButton.onClick.AddListener(NewGame);
        _adjustButton.onClick.AddListener(ToggleAdjust);
        _settingsButton.onClick.AddListener(ToggleSettings);

        _adjustExitButton.onClick.AddListener(ToggleAdjust);
        _settingsExitButton.onClick.AddListener(ToggleSettings);
        _quitButton.onClick.AddListener(QuitGame);

        _settingsExitButton.enabled = false;
        _adjustExitButton.enabled = false;
        _sfxSlider.enabled = false;

        PlayerBehavior.CameraPosition = new Vector3(0, 0.85f, 0.65f);
        PlayerBehavior.Volume = _sfxSlider.value / 100;
    }
    private void Update()
    {
        if (_adjustScreen.enabled)
        {
            Vector2 posY = Movement.input.Controller.Move.ReadValue<Vector2>();
            Vector2 posX = Movement.input.Controller.Vertical.ReadValue<Vector2>();
            _camera.transform.localPosition += new Vector3(0, posY.y * 0.01f, posX.y * 0.01f);
        }
    }
    private void NewGame()
    {
        _uiSource.Play();
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
    private void ToggleAdjust()
    {
        _uiSource.Play();
        SwitchScreen(_adjustScreen);

        _adjustExitButton.enabled = !_adjustExitButton.enabled;

        if (_menuScreen.enabled)
        {
            _adjustButton.Select();
            PlayerBehavior.CameraPosition = _camera.transform.localPosition;
        }
        else
        {
            _adjustExitButton.Select();
        }
    }
    private void ToggleSettings()
    {
        _uiSource.Play();
        SwitchScreen(_settingsScreen);
        
        _settingsExitButton.enabled = !_settingsExitButton.enabled;
        _sfxSlider.enabled = !_sfxSlider.enabled;

        if (_menuScreen.enabled)
        {
            _settingsButton.Select();
        }
        else
        {
            _settingsExitButton.Select();
        }
    }
    private void QuitGame()
    {
        _uiSource.Play();
        Application.Quit();
    }
    private void SwitchScreen(Canvas screen)
    {
        _menuScreen.enabled = !_menuScreen.enabled;
        screen.enabled = !screen.enabled;

        _newGameButton.enabled = !_newGameButton.enabled;
        _adjustButton.enabled = !_adjustButton.enabled;
        _settingsButton.enabled = !_settingsButton.enabled;
        _quitButton.enabled = !_quitButton.enabled;
    }
    private IEnumerator LoadDelay()
    {
        _newGameButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        _newGameButton.interactable = true;
    }
    public void SliderValueChanged()
    {
        PlayerBehavior.Volume = _sfxSlider.value / 100;
    }
}
