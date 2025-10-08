using Futurift.DataSenders;
using Futurift.Options;
using Futurift;
using UnityEngine;
using System.Collections;

public class MechaController : MonoBehaviour
{
    [SerializeField] private string ipAddress = "127.0.0.1";
    [SerializeField] private int port = 6065;
    private FutuRiftController Controller;
    private Vector2 _playerMove;
    private Vector2 _playerVertical;

    private void Awake()
    {
        var udpOptions = new UdpOptions
        {
            ip = ipAddress,
            port = port
        };

        Controller = new FutuRiftController(new UdpPortSender(udpOptions));
    }

    private void Update()
    {
        _playerMove = Movement.input.Controller.Move.ReadValue<Vector2>();
        _playerVertical = Movement.input.Controller.Vertical.ReadValue<Vector2>();
        Controller.Pitch = (_playerMove.y * 3f + _playerVertical.y * 1.5f) * 2f;
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
