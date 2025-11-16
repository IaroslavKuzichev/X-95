using Futurift.DataSenders;
using Futurift.Options;
using Futurift;
using UnityEngine;
using Unity.XR.CoreUtils;

public class MechaController : MonoBehaviour
{
    [SerializeField] private string ipAddress = "127.0.0.1";
    [SerializeField] private int port = 6065;
    private FutuRiftController Controller;
    private Vector3 prevVelocity;

    private void Awake()
    {
        var udpOptions = new UdpOptions
        {
            ip = ipAddress,
            port = port
        };

        prevVelocity = Vector3.zero;
        Controller = new FutuRiftController(new UdpPortSender(udpOptions));
    }

    private void FixedUpdate()
    {
        Vector3 currentVelocity = transform.InverseTransformDirection(Movement.rb.linearVelocity);
        Vector3 a = ((currentVelocity - prevVelocity) / Time.deltaTime).Abs();
        Vector3 acceleration = new Vector3(a.x * currentVelocity.normalized.x, a.y * currentVelocity.normalized.y, a.z * currentVelocity.normalized.z);
        Controller.Pitch = (acceleration.z + acceleration.y) * 3f;
        Controller.Roll = (acceleration.x * 0.5f + Movement.playerTurn.x * 2f) * 3f;
        prevVelocity = currentVelocity;
    }
    //private void Update()
    //{
    //    Vector3 velocity = transform.InverseTransformDirection(Movement.rb.linearVelocity);
    //    Controller.Pitch = (velocity.z + velocity.y) * 3f;
    //    Controller.Roll = (velocity.x + Movement.playerTurn.x) * 3f;
    //}

    private void OnEnable()
    {
        Controller?.Start();
    }

    private void OnDisable()
    {
        Controller?.Stop();
    }
}
