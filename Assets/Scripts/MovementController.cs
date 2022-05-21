using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    float playerVelocity;
    [SerializeField] float walkVelocity = 5f;
    [SerializeField] float runVelocity = 10f;

    [SerializeField] float rotationSpeed = 600f;

    Rigidbody rigidbody;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerVelocity = runVelocity;
        }

        else
        {
            playerVelocity = walkVelocity;
        }

        float xPosition = Input.GetAxis("Horizontal") * Time.deltaTime * playerVelocity;
        float zPosition = Input.GetAxis("Vertical") * Time.deltaTime * playerVelocity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;

        transform.Rotate(0, mouseX, 0);
        transform.Translate(xPosition, 0, zPosition);
    }
}