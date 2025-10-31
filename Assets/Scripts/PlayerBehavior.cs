using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
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
    }
    
}
