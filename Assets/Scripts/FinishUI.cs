using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{
    [SerializeField] Button _exitButton;
    [SerializeField] Button _restartButton;
    private void Awake()
    {
        StartCoroutine(LoadDelay());
        _exitButton.onClick.AddListener(ExitGame);
        _restartButton.onClick.AddListener(RestartLevel);
    }
    private void ExitGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene("SampleLevel", LoadSceneMode.Single);
    }
    private IEnumerator LoadDelay()
    {
        _exitButton.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _exitButton.enabled = true;
    }
}
