using Futurift.DataSenders;
using Futurift.Options;
using Futurift;
using UnityEngine;

public class MechaController : MonoBehaviour
{
    [SerializeField] private string ipAddress = "127.0.0.1";
    [SerializeField] private int port = 6065;
    private FutuRiftController Controller;

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
        Controller.Pitch = (-Movement.rb.linearVelocity.z + Movement.rb.linearVelocity.y) * 3f;
        Controller.Roll = (Movement.rb.linearVelocity.x) * 3f;
    }

    private void OnEnable()
    {
        Controller?.Start();
    }

    private void OnDisable()
    {
        Controller?.Stop();
    }
}
