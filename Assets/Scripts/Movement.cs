using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private GameObject _joystick;
    static public CustomInput input;
    static public Rigidbody rb;
    static public Vector2 playerMove;
    static public Vector2 playerVertical;
    static public Vector2 playerTurn;
    private void Awake()
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody>();
        if (gameObject.scene.name == "MainMenu" || gameObject.scene.name == "FinishScene")
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
            playerMove = input.Controller.Move.ReadValue<Vector2>();
            playerVertical = input.Controller.Vertical.ReadValue<Vector2>();
            playerTurn = input.Controller.Turn.ReadValue<Vector2>();
            Vector3 _movement = new Vector3(playerMove.x, playerVertical.y * 0.5f, playerMove.y);
            rb.AddRelativeForce(_movement * _playerSpeed);
            _joystick.gameObject.transform.localRotation = Quaternion.Euler(-playerVertical.y * 15f + 45, 0, -playerMove.x * 20f);
            if (Math.Abs(playerTurn.x) > 0f)
            {
                transform.Rotate(0, playerTurn.x * 0.25f, 0);
            } 
            //transform.rotation = Quaternion.Euler(_playerVertical.y * 1.5f, transform.rotation.y, -_playerMove.x * 4f);
        }
    }
}
