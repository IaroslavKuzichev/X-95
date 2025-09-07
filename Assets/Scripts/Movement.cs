using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    private CustomInput _input;
    private Vector2 _playerMove;
    private Vector2 _playerVertical;

    private void Awake()
    {
        _input = new CustomInput();
        _input.Controller.FireLeftCannon.performed += ctx => FireLeft();
        _input.Controller.FireRightCannon.performed += ctx => FireRight();
        _input.Controller.Boost.performed += ctx => Boost();
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
    }

    private void FireLeft()
    {
        Debug.Log("Выстрел из левой пушки");
    }

    private void FireRight()
    {
        Debug.Log("Выстрел из правой пушки");
    }

    private void Boost()
    {
        _playerSpeed = 2;
    }
}
