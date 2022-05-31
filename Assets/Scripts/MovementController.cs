using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float playerVelocity = 400f;
    [SerializeField] float rotationSpeed = 800f;
    [SerializeField] float sprint = 1.5f;

    Vector3 movement;
    Vector3 walkingMovement;
    Vector3 sprintingMovement;

    public Vector3 turn;

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
        float walkingHorizontalAxis = Input.GetAxis("Horizontal") * Time.fixedDeltaTime
                    * playerVelocity;

        float walkingVerticalAxis = Input.GetAxis("Vertical") * Time.fixedDeltaTime
            * playerVelocity;

        float sprintHorizontalAxis = walkingHorizontalAxis * sprint;

        float sprintVerticalAxis = walkingVerticalAxis * sprint;

        walkingMovement = new Vector3(walkingHorizontalAxis, transform.position.y, walkingVerticalAxis);
        sprintingMovement = new Vector3(sprintHorizontalAxis, transform.position.y
            , sprintVerticalAxis);
        
    }

    void PerformMovement(Vector3 movement)
    {
        if (!CollisionHandler.dead)
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
    }

    void Rotate()
    {
        turn.x += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
}