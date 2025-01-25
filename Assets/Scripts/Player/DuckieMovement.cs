using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DuckieMovement : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction sprintAction;

    public Transform Motor;
    public float Steerpower;
    public float Power;
    public float MaxSpeed;
    public float MinSpeed;
    public float Drag;
    public float AccelerationSpeed;
    public float SteerAccelerationSpeed;

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
        sprintAction = playerInput.actions.FindAction("Sprint");
        Rigidbody = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
    }

    private void FixedUpdate()
    {

        //Boat Controls
        var forceDirection = transform.forward;
        var steer = 0;

        moveDirection = moveAction.ReadValue<Vector2>();

        //Debug.Log(moveDirection);

        if (moveDirection.x > 0)
        {
            steer = -1;
        }
        else if (moveDirection.x < 0)
        {
            steer = 1;
        }
         //Left and Right turning
        Rigidbody.AddForceAtPosition(steer * transform.right * Steerpower / 100f, Motor.position);

        //Back and Forth movement
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

        if (sprintAction.IsPressed() == true)
        {
            if (Power >= MaxSpeed)
            {
                Power = MaxSpeed;
                Steerpower = 1000f;
            }
            else
            {
                Power = Power + (Time.deltaTime * AccelerationSpeed);
                Steerpower = Steerpower - (Time.deltaTime * SteerAccelerationSpeed);
            }
          
        }
        else
        {
            if (Power <= MinSpeed)
            {
                Power = MinSpeed;
                Steerpower = 2000f;
            }
            else
            {
                Power = Power - (Time.deltaTime * AccelerationSpeed);
                Steerpower = Steerpower + (Time.deltaTime * SteerAccelerationSpeed);
            }
        }

        //Fake Bobbing
        float xRot = (Mathf.Sin(Time.time * bobRotSpeed) * bobRotAmount);
        float zRot = (Mathf.Sin((Time.time - 1.0f) * bobRotSpeed) * bobRotAmount);
        transform.eulerAngles = new Vector3(xRot, transform.eulerAngles.y, zRot);

    }
}