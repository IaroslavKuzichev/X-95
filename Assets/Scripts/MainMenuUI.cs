using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
//using UnityEditor;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _settingsExitButton;

    [SerializeField] private Canvas _settingsScreen;
    [SerializeField] private Canvas _menuScreen;

    private void Awake()
    {
        StartCoroutine(LoadDelay());
        _newGameButton.onClick.AddListener(NewGame);
        _continueButton.onClick.AddListener(Continue);
        _settingsButton.onClick.AddListener(EnterSettings);
        _settingsExitButton.onClick.AddListener(ExitSettings);
        _quitButton.onClick.AddListener(QuitGame);
    }
    private void NewGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    private void Continue()
    {
        Debug.Log("Continue");
    }
    private void EnterSettings()
    {
        _menuScreen.enabled = false;
        _settingsScreen.enabled = true;
        _settingsExitButton.Select();
    }
    private void ExitSettings()
    {
        _menuScreen.enabled = true;
        _settingsScreen.enabled = false;
        _newGameButton.Select();
    }
    private void QuitGame()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
    private IEnumerator LoadDelay()
    {
        _newGameButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        _newGameButton.interactable = true;
    }
}
