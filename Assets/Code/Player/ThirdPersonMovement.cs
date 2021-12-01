using UnityEngine;

/*Movement Control for players: this class handles 3rd person movement*/
public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    Transform cam;

    public float speed = 5f;
    public float jumpSpeed = 4;

    float gravity = 9.8f;
    float vSpeed = 0; // current vertical velocity

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    int jump_counter = 0; // control consecutive amount of jumps

    private void Start()
    {
        cam = Camera.main.transform;
        jump_counter = 0;
    }


    //[Client]
    void Update()
    {
        //if (!isLocalPlayer) { return; }

        CmdMove();
    }

    //[Command]
    private void CmdMove() {
        //TODO: some form of validation here... will put later
        RpcMove();
    }

    //[ClientRpc]
    private void RpcMove()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float verticle = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, verticle).normalized;

        if (controller.isGrounded)
        {
            vSpeed = 0; // grounded character has vSpeed = 0...
            jump_counter = 0;
        }

        if (Input.GetKeyDown("space") & jump_counter < 2) { // unless it jumps:
            vSpeed = jumpSpeed;
            jump_counter++;
        }

        Vector3 moveDirection = new Vector3(0, 0, 0);
        vSpeed -= gravity * Time.deltaTime; // apply gravity acceleration to vertical speed
        moveDirection.y = vSpeed; // include vertical speed in vel
        controller.Move(moveDirection * Time.deltaTime);

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }


    }

}
