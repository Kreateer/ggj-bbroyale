using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DuckieMovement : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction sprintAction;
    InputAction anchorAction;

    public Transform Motor;

    public float Steerpower;
    public float MaxSteerpower;
    public float MinSteerpower;
    public float BaseSteerpower;

    public float Speed;
    public float MaxSpeed;
    public float MinSpeed;
    public float BaseSpeed;

    public float AccelerationSpeed;
    public float SteerAccelerationSpeed;

    public float BreakSpeed;
    public float BreakSpeedSteer;

    public bool isMovingForward;

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
        anchorAction = playerInput.actions.FindAction("Jump");
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
            Rigidbody.AddForce(forward * Speed, ForceMode.Impulse);
            isMovingForward = true;
        }
        else if (moveDirection.y < 0)
        {
            Rigidbody.AddForce(backwards * Speed, ForceMode.Impulse);
            isMovingForward = false;
        }
        else {
            isMovingForward = false;
        }

        //Boat sprinting go brrrrrrrt
        if (sprintAction.IsPressed() == true && isMovingForward == true)
        {
            //If speed less then maxx, and steer more then min, GO FULL BOOST!
            if (Speed <= MaxSpeed)
            {
                Speed = Speed + (Time.deltaTime * AccelerationSpeed);
            }
            if (Steerpower >= MinSteerpower)
            {
                Steerpower = Steerpower - (Time.deltaTime * SteerAccelerationSpeed);
            }
        }
        else if (anchorAction.IsPressed() == true && sprintAction.IsPressed() == false)
        {
            if (Speed >= MinSpeed)
            {
                Speed = Speed - (Time.deltaTime * BreakSpeed);
            }

            if (Steerpower <= 6000f)
            {
                Steerpower = Steerpower + (Time.deltaTime * BreakSpeedSteer);
            }
        }
        else
        {
            //utilizing to go back to average speeds
            if (Speed != BaseSpeed)
            {
                //checking to see if higher or less then base, and then correcting
                if (Speed >= BaseSpeed)
                {
                    Speed = Speed - (Time.deltaTime * AccelerationSpeed);
                }
                else
                {
                    Speed = Speed + (Time.deltaTime * AccelerationSpeed);
                }
            }
            if (Steerpower != BaseSteerpower) 
            { 
                //checking to see if steerpower is higher or less then base, then correcting.
                if (Steerpower >= BaseSteerpower)
                {
                    Steerpower = Steerpower - (Time.deltaTime * SteerAccelerationSpeed);
                }
                else
                {
                    Steerpower = Steerpower + (Time.deltaTime * SteerAccelerationSpeed);
                }
                
                if (Steerpower >= MaxSteerpower)
                {
                    Steerpower = Steerpower - (Time.deltaTime * BreakSpeedSteer);
                }
            } 
        }

       // else
        //{
          //  Speed = Speed - (Time.deltaTime * AccelerationSpeed);
            //Steerpower = Steerpower + (Time.deltaTime * SteerAccelerationSpeed);
        //}

        

        //Fake Bobbing
        float xRot = (Mathf.Sin(Time.time * bobRotSpeed) * bobRotAmount);
        float zRot = (Mathf.Sin((Time.time - 1.0f) * bobRotSpeed) * bobRotAmount);
        transform.eulerAngles = new Vector3(xRot, transform.eulerAngles.y, zRot);

    }
}