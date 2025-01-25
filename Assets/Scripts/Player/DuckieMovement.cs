using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DuckieMovement : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction;

    public Transform Motor;
    public float Steerpower = 3500f;
    public float Power = 1f;
    public float MaxSpeed = 10f;
    public float Drag = 0.1f;

    protected Rigidbody Rigidbody;
    protected Quaternion StartRotation;

    Vector2 moveDirection = Vector2.zero;

    public float xRot;
    public float zRot;

    public float bobRotSpeed;
    public float bobRotAmount;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        Rigidbody = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
    }

    private void FixedUpdate()
    {
        var forceDirection = transform.forward;
        var steer = 0;

        moveDirection = moveAction.ReadValue<Vector2>();

        Debug.Log(moveDirection);

        if (moveDirection.x > 0)
        {
            steer = -1;
        }
        else if (moveDirection.x < 0)
        {
            steer = 1;
        }

        Rigidbody.AddForceAtPosition(steer * transform.right * Steerpower / 100f, Motor.position);

        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        var backwards = Vector3.Scale(new Vector3(-1, 0, -1), transform.forward);

        if (moveDirection.y > 0)
        {
            Rigidbody.AddForce(forward * Power, ForceMode.Impulse);
          
        }
        else if (moveDirection.y < 0)
        {
            Rigidbody.AddForce(backwards * Power, ForceMode.Impulse);
        }

        float xRot = (Mathf.Sin(Time.time * bobRotSpeed) * bobRotAmount);

        float zRot = (Mathf.Sin((Time.time - 1.0f) * bobRotSpeed) * bobRotAmount);

        transform.eulerAngles = new Vector3(xRot, transform.eulerAngles.y, zRot);

    }
}