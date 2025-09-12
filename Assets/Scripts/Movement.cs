using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    private CustomInput _input;
    private Vector2 _playerMove;
    private Vector2 _playerVertical;
    private void Awake()
    {
        _input = new CustomInput();
    }
    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    private void Update()
    {
        _playerMove = _input.Controller.Move.ReadValue<Vector2>();
        _playerVertical = _input.Controller.Vertical.ReadValue<Vector2>();
        Vector3 _movement = new Vector3(_playerMove.x, _playerVertical.y * 0.5f, _playerMove.y);
        transform.position += _movement * _playerSpeed * Time.deltaTime * 5f;
        transform.rotation = Quaternion.Euler((_playerMove.y * 2f + _playerVertical.y), 0, -_playerMove.x * 2f);
    }
}
