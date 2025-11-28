using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _camera; 

    static private int _playerHP;
    static private int _colectibleCount; 
    static public Vector3 CameraPosition;
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
    static public int CollectibleCount
    {
        get => _colectibleCount;
        set
        {
            if (value < 0) _colectibleCount = 0;
            else _colectibleCount = value;
        }
    }
    private void Start()
    {
        CollectibleCount = 0;
        PlayerHP = 100;
        _camera.transform.localPosition = CameraPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish") && CollectibleCount == 2)
        {
            SceneManager.LoadScene("FinishScene");
        }
    }
}
