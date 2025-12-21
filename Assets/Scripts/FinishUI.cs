using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{
    [SerializeField] Button _nextButton;
    [SerializeField] Button _restartButton;
    [SerializeField] private AudioSource _uiSource;
    private string _nextLevel;
    private void Awake()
    {
        StartCoroutine(LoadDelay());
        _nextButton.onClick.AddListener(NextLevel);
        _restartButton.onClick.AddListener(RestartLevel);
        switch (PlayerBehavior.CurrentLevel)
        {
            case "Level1":
                _nextLevel = "Level2";
                break;
            case "Level2":
                _nextLevel = "Level3";
                break;
            case "Level3":
                _nextLevel = "Level4";
                break;
            case "Level4":
                _nextLevel = "MainMenu";
                break;
            default:
                _nextLevel = PlayerBehavior.CurrentLevel;
                break;
        }
    }
    private void NextLevel()
    {
        _uiSource.Play();
        SceneManager.LoadScene(_nextLevel, LoadSceneMode.Single);
    }
    private void RestartLevel()
    {
        _uiSource.Play();
        SceneManager.LoadScene(PlayerBehavior.CurrentLevel, LoadSceneMode.Single);
    }
    private IEnumerator LoadDelay()
    {
        _nextButton.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _nextButton.enabled = true;
    }
}
