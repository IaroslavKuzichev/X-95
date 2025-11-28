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
    private Vector3 currentAcceleration;
    private Vector3 acceleration;

    private void Awake()
    {
        var udpOptions = new UdpOptions
        {
            ip = ipAddress,
            port = port
        };

        prevVelocity = Vector3.zero;
        currentAcceleration = Vector3.zero;
        Controller = new FutuRiftController(new UdpPortSender(udpOptions));
    }
    //private void FixedUpdate()
    //{
    //    Vector3 currentVelocity = transform.InverseTransformDirection(Movement.rb.linearVelocity);
    //    Vector3 a = ((currentVelocity - prevVelocity) / Time.deltaTime).Abs();
    //    acceleration = new Vector3(a.x * currentVelocity.normalized.x, a.y * currentVelocity.normalized.y, a.z * currentVelocity.normalized.z);
    //    prevVelocity = currentVelocity;
    //}
    private void FixedUpdate()
    {
        Vector3 currentVelocity = transform.InverseTransformVector(Movement.rb.linearVelocity);
        Vector3 a = ((currentVelocity - prevVelocity) / Time.deltaTime).Abs();
        acceleration = new Vector3(a.x * currentVelocity.normalized.x, a.y * currentVelocity.normalized.y, a.z * currentVelocity.normalized.z);
        prevVelocity = currentVelocity;
        currentAcceleration = Vector3.Lerp(currentAcceleration, acceleration, 0.01f);
        Controller.Pitch = (-currentAcceleration.z + currentAcceleration.y) * 5f;
        Controller.Roll = (currentAcceleration.x * 0.5f + Movement.playerTurn.x* 2f) * 5f;
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
