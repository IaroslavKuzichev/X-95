using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    static public CustomInput input;
    private Vector2 _playerMove;
    private Vector2 _playerVertical;
    private void Awake()
    {
        input = new CustomInput();
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
        transform.position += _movement * _playerSpeed * Time.deltaTime * 5f;
        transform.rotation = Quaternion.Euler(_playerVertical.y * 1.5f, 0, -_playerMove.x * 4f);
    }
}
