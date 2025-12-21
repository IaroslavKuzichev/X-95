using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    [SerializeField] Button _exitButton;
    [SerializeField] Button _restartButton;
    [SerializeField] private AudioSource _uiSource;
    private void Awake()
    {
        StartCoroutine(LoadDelay());
        _exitButton.onClick.AddListener(ExitGame);
        _restartButton.onClick.AddListener(RestartLevel);
    }
    private void ExitGame()
    {
        _uiSource.Play();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    private void RestartLevel()
    {
        _uiSource.Play();
        SceneManager.LoadScene(PlayerBehavior.CurrentLevel, LoadSceneMode.Single);
    }
    private IEnumerator LoadDelay()
    {
        _exitButton.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _exitButton.enabled = true;
    }
}
