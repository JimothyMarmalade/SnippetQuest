using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //References to Components/other objects
    public CharacterController Controller;
    public Transform Cam;

    public Transform GroundCheck;
    public float GroundDistance = 0.2f;
    public LayerMask groundMask;
    //--------------------
    //Variables to be changed by the Developer
    public float Speed = 6;
    public float JumpHeight = 4;
    public float ForceGravity = -9.81f;
    public float TurnSmoothTime = 0.1f;
    //--------------------

    Vector3 PlayerVelocity;
    float SmoothTurnVelocity;
    bool PlayerIsGrounded;



    // Update is called once per frame
    void Update()
    {
        PlayerIsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

        if (PlayerIsGrounded && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = -2f;
        }


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref SmoothTurnVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDirection.normalized * Speed * Time.deltaTime);
        }
        //Jump
        if (Input.GetButtonDown("Jump") && PlayerIsGrounded)
        {
            PlayerVelocity.y = Mathf.Sqrt(JumpHeight * -2f * ForceGravity);
        }
        //Gravity
        PlayerVelocity.y += ForceGravity * Time.deltaTime;
        Controller.Move(PlayerVelocity * Time.deltaTime);
    }
}
