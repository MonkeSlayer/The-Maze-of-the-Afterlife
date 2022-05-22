using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float playerVelocity = 600f;
    [SerializeField] float rotationSpeed = 1000f;
    [SerializeField] float sprint = 2f;

    Vector3 movement;
    Vector3 walkingMovement;
    Vector3 sprintingMovement;

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
        GetMovement();
        Rotate();
    }

    void FixedUpdate()
    {
        PerformMovement(walkingMovement);
    }

    void GetMovement()
    {
        float walkingHorizontalAxis = Input.GetAxis("Horizontal") * Time.deltaTime
                    * playerVelocity;

        float walkingVerticalAxis = Input.GetAxis("Vertical") * Time.deltaTime
            * playerVelocity;

        float sprintHorizontalAxis = walkingHorizontalAxis * sprint;

        float sprintVerticalAxis = walkingVerticalAxis * sprint;

        walkingMovement = new Vector3(walkingHorizontalAxis, transform.position.y, walkingVerticalAxis);
        sprintingMovement = new Vector3(sprintHorizontalAxis, transform.position.y
            , sprintVerticalAxis);
        
    }

    void PerformMovement(Vector3 movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = sprintingMovement;
        }

        else
        {
            movement = walkingMovement;
        }

        rigidbody.velocity = transform.TransformDirection(movement);
    }

    void Rotate()
    {
        float mousePosition = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        Vector3 mouseX = new Vector3(0, mousePosition, 0);
        Quaternion rotation = Quaternion.Euler(mouseX);

        rigidbody.MoveRotation(rigidbody.rotation * rotation);
    }
}