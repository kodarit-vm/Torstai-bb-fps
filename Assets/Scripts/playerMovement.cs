using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private CharacterController controller;
    private float gravity = -9.81f;

    private float moveSpeed = 8f, runSpeed = 1.8f;
    private float pushForce = 5f;

    private float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 4.5f;

    private Vector3 move;
    private Vector3 velocity;

    public LayerMask groundMask;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        move = transform.right * xAxis + transform.forward * zAxis;

        if (Input.GetButton("Fire3"))
        {
            controller.Move(move * moveSpeed * runSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.layer == 6)
        {
            return;
        }

        Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();

        if (rb == null)
        {
            return;
        }

        rb.AddForce(move * pushForce);
    }
}
