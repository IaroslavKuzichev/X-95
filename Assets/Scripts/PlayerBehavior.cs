using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _camera; 
    [SerializeField] private TextMeshProUGUI _weaponText;
    [SerializeField] private TextMeshProUGUI _engineText;

    private static int _playerHP;
    private static int _colectibleCount;

    public static float Volume;
    public static Vector3 CameraPosition;

    public static bool WeaponsDamaged;
    public static bool EnginesDamaged;
    public static int PlayerHP
    {
        get => _playerHP;
        set
        {
            if (value < 0)
            {
                _playerHP = 0;
            }
            else if (value > 100)
            {
                _playerHP = 100;
            }
            else
            {
                _playerHP = value;
            }
        }
    }
    public static int CollectibleCount
    {
        get => _colectibleCount;
        set
        {
            if (value < 0)
            {
                _colectibleCount = 0;
            }
            else
            {
                _colectibleCount = value;
            }
        }
    }
    private void Start()
    {
        WeaponsDamaged = false;
        EnginesDamaged = false;
        CollectibleCount = 0;
        PlayerHP = 100;
        _camera.transform.localPosition = CameraPosition;
        foreach (AudioSource audSrc in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            audSrc.volume = Volume;
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish") && CollectibleCount == 2)
        {
            SceneManager.LoadScene("FinishScene");
        }
    }
    private void Update()
    {
        if (WeaponsDamaged)
        {
            _weaponText.text = "Повреждение орудий";
        }
        else
        {
            _weaponText.text = "";
        }

        if (EnginesDamaged)
        {
            _engineText.text = "Повреждение двигателя";
        }
        else
        {
            _engineText.text = "";
        }
    }
}
