using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Camera playerCamera;

    public float walkSpeed = 10f;
    public float lookSensitivity = 10f;
    public float jumpSpeed = 10f;
    public float gravity = 9.8f;
    public bool grounded;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 velocity;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMovement();
        grounded = controller.isGrounded;
    }

    private void PlayerMovement()
    {
        // Player rotation
        if(Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse X") < 0)
        {
            this.gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime, 0);
        }

        // Camera rotation
        if(Input.GetAxis("Mouse Y") > 0 || Input.GetAxis("Mouse Y") < 0)
        {
            playerCamera.gameObject.transform.Rotate(Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime, 0, 0);
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = this.transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime * walkSpeed);

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }
    }
}
