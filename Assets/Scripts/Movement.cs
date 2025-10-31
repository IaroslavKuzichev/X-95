using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private GameObject _joystick;
    static public CustomInput input;
    static public Rigidbody rb;
    private Vector2 _playerMove;
    private Vector2 _playerVertical;
    private void Awake()
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    private void Update()
    {
        _playerMove = input.Controller.Move.ReadValue<Vector2>();
        _playerVertical = input.Controller.Vertical.ReadValue<Vector2>();
        Vector3 _movement = new Vector3(_playerMove.x, _playerVertical.y * 0.5f, _playerMove.y);
        rb.AddForce(_movement * _playerSpeed);
        _joystick.gameObject.transform.rotation = Quaternion.Euler(-_playerVertical.y * 15f, 0, -_playerMove.x * 20f);
        transform.rotation = Quaternion.Euler(_playerVertical.y * 1.5f, 0, -_playerMove.x * 4f);
    }
}
