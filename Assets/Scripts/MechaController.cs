using Futurift.DataSenders;
using Futurift.Options;
using Futurift;
using UnityEngine;
using System.Collections;
using UnityEngine.Windows;

public class MechaController : MonoBehaviour
{
    [SerializeField] private string ipAddress = "127.0.0.1";
    [SerializeField] private int port = 6065;
    private FutuRiftController Controller;
    private Vector2 _playerMove;
    private Vector2 _playerVertical;
    //private Vector3 _playerAcceleration;
    //private Vector3 _prevPosition;
    //private Vector3 _curVelocity;
    //private Vector3 _prevVelocity;

    private void Awake()
    {
        var udpOptions = new UdpOptions
        {
            ip = ipAddress,
            port = port
        };

        Controller = new FutuRiftController(new UdpPortSender(udpOptions));
        //_prevVelocity = new Vector3(0, 0, 0);
        //_prevPosition = transform.position;
    }

    private void Update()
    {
        //_curVelocity = (transform.position - _prevPosition) / Time.deltaTime;
        //_playerAcceleration = (_curVelocity - _prevVelocity) / Time.deltaTime;
        //Controller.Pitch = _playerAcceleration.z * 0.5f;
        //Controller.Roll = _playerAcceleration.x * 0.5f;
        //Debug.Log($"Пред. Скорость: {_prevPosition}");
        //_prevVelocity = _curVelocity;
        //_prevPosition = transform.position;
        //Debug.Log($"Тек Скорость: {transform.position}");
        //Debug.Log($"Ускорение: {_playerAcceleration}");
        _playerMove = Movement.input.Controller.Move.ReadValue<Vector2>();
        _playerVertical = Movement.input.Controller.Vertical.ReadValue<Vector2>();
        Controller.Pitch = _playerMove.y * 6f + _playerVertical.y * 3f;
        Controller.Roll = _playerMove.x * 6f;
    }

    private void OnEnable()
    {
        Controller?.Start();
    }

    private void OnDisable()
    {
        Controller?.Stop();
    }
    public static IEnumerator TorpedoShake()
    {
        Movement.input.Controller.Torpedo.Disable();
        Debug.Log("Begin of wait");
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("End of wait");
        Movement.input.Controller.Torpedo.Enable();
    }
}
