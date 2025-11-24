using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;
//using UnityEditor;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _instructionsButton;
    [SerializeField] private Button _settingsButton;

    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _settingsExitButton;
    [SerializeField] private Button _instructionsExitButton;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [SerializeField] private Canvas _settingsScreen;
    [SerializeField] private Canvas _instructionsScreen;
    [SerializeField] private Canvas _menuScreen;

    private void Awake()
    {
        StartCoroutine(LoadDelay());
        _newGameButton.onClick.AddListener(NewGame);
        _instructionsButton.onClick.AddListener(ToggleInstructions);
        _settingsButton.onClick.AddListener(ToggleSettings);

        _instructionsExitButton.onClick.AddListener(ToggleInstructions);
        _settingsExitButton.onClick.AddListener(ToggleSettings);
        _quitButton.onClick.AddListener(QuitGame);

        _settingsExitButton.enabled = false;
        _instructionsExitButton.enabled = false;
        _musicSlider.enabled = false;
        _sfxSlider.enabled = false;
    }
    private void NewGame()
    {
        SceneManager.LoadScene("SampleLevel", LoadSceneMode.Single);
    }
    private void ToggleInstructions()
    {
        SwitchScreen(_instructionsScreen);

        _instructionsExitButton.enabled = !_instructionsExitButton.enabled;

        if (_menuScreen.enabled)
        {
            _instructionsButton.Select();
        }
        else
        {
            _instructionsExitButton.Select();
        }
    }
    private void ToggleSettings()
    {
        SwitchScreen(_settingsScreen);
        
        _settingsExitButton.enabled = !_settingsExitButton.enabled;
        _musicSlider.enabled = !_musicSlider.enabled;
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
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
    private void SwitchScreen(Canvas screen)
    {
        _menuScreen.enabled = !_menuScreen.enabled;
        screen.enabled = !screen.enabled;

        _newGameButton.enabled = !_newGameButton.enabled;
        _instructionsButton.enabled = !_instructionsButton.enabled;
        _settingsButton.enabled = !_settingsButton.enabled;
        _quitButton.enabled = !_quitButton.enabled;
    }
    private IEnumerator LoadDelay()
    {
        _newGameButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        _newGameButton.interactable = true;
    }
}
