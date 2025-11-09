using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _settingsExitButton;

    [SerializeField] private Canvas _settingsCanvas;

    private void Awake()
    {
        _newGameButton.onClick.AddListener(NewGame);
        _continueButton.onClick.AddListener(Continue);
        _settingsButton.onClick.AddListener(Settings);
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
    private void Settings()
    {
        _settingsCanvas.enabled = true;
    }
    private void QuitGame()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
