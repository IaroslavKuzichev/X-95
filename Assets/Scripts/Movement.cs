using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private GameObject _joystick;
    static public CustomInput input;
    static public Rigidbody rb;
    private Vector2 _playerMove;
    private Vector2 _playerVertical;
    private Vector2 _playerTurn;
    private void Awake()
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody>();
        if (gameObject.scene.name == "MainMenu")
        {
            _playerSpeed = 0;
        }
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
        if (_playerSpeed != 0)
        {
            _playerMove = input.Controller.Move.ReadValue<Vector2>();
            _playerVertical = input.Controller.Vertical.ReadValue<Vector2>();
            _playerTurn = input.Controller.Turn.ReadValue<Vector2>();
            Vector3 _movement = new Vector3(_playerMove.x, _playerVertical.y * 0.5f, _playerMove.y);
            rb.AddRelativeForce(_movement * _playerSpeed);
            _joystick.gameObject.transform.rotation = Quaternion.Euler(-_playerVertical.y * 15f, 0, -_playerMove.x * 20f);
            if (Math.Abs(_playerTurn.x) > 0f)
            {
                transform.Rotate(0, _playerTurn.x * 0.25f, 0);
            } 
            //transform.rotation = Quaternion.Euler(_playerVertical.y * 1.5f, transform.rotation.y, -_playerMove.x * 4f);
        }
    }
}
