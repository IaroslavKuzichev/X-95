using System;
using Bhaptics.SDK2;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public float PlayerSpeed;
    [SerializeField] private GameObject _joystick;

    private Vector2 playerMove;
    private Vector2 playerVertical;

    public static CustomInput input;
    public static Rigidbody rb;
    public static Vector2 playerTurn;

    private void Awake()
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody>();
        if (!gameObject.scene.name.StartsWith("L"))
        {
            PlayerSpeed = 0;
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
        if (PlayerSpeed != 0)
        {
            playerMove = input.Controller.Move.ReadValue<Vector2>();
            playerVertical = input.Controller.Vertical.ReadValue<Vector2>();
            playerTurn = input.Controller.Turn.ReadValue<Vector2>();
            Vector3 _movement = new Vector3(playerMove.x, playerVertical.y * 0.5f, playerMove.y);
            rb.AddRelativeForce(_movement * PlayerSpeed);
            _joystick.gameObject.transform.localRotation = Quaternion.Euler(-playerVertical.y * 15f + 45, 0, -playerMove.x * 20f);
            if (Math.Abs(playerTurn.x) > 0f)
            {
                transform.Rotate(0, playerTurn.x * PlayerSpeed * 0.05f, 0);
            }

            if (rb.linearVelocity.x > 0.2f || rb.linearVelocity.y > 0.2f || rb.linearVelocity.z > 0.2f)
            {
                BhapticsLibrary.Play("movement");
            }
            else 
            {
                BhapticsLibrary.StopByEventId("movement");
            }
        }
    }
}
