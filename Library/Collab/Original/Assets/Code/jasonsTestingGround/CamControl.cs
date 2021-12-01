using UnityEngine;

public class CamControl : MonoBehaviour
{
    //NOTE you must have things tagged with ground, and your player models with a 
    //child that acts as the point that touches the ground collider, right below ur feet
    float Speed = 10f;//affects movement speed
    float JumpHeight = 3f;//affects how high you jump
    float Gravity = -9.81f;//affects how fast you fall down
    float GroundDistance = 0.1f;//distance between feet and floor
    float DashDistance = 5f;//how far you dash
    public LayerMask Ground;//varibale to tag things that should prevent gravity acceleration
    public Vector3 Drag;//drag on falling and moving
    float dashTimer = 0;
    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    Vector3 moveDir;

    public bool enabledMove = true;
    public Transform Cam;
    float smoothTime = 0.1f;
    float smoothVel;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _groundChecker = transform.GetChild(0);
        //Cursor.lockState = CursorLockMode.Locked;

        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible=true;
    }
    public void speedUp(){
        Speed = Speed*2;
    }
    public void jumpUp(){
        JumpHeight = JumpHeight*2;
    }
    public void dashUp(){
        DashDistance = DashDistance*2;
    }
    public void lowerGravity(){
        _velocity.y -= 2;
        _velocity += Vector3.Scale(moveDir.normalized*-1, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
    }
    void Update()
    {
        dashTimer+=Time.deltaTime;
        //REMEMBER TO SET THINGS TO GROUND in their layer settings
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

          float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horiz, 0f, vert).normalized;

        if (enabled)
        {
            if (direction.magnitude >= 0.1f)
            {
                float playerAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, playerAngle, ref smoothVel, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir = Quaternion.Euler(0f, playerAngle, 0f) * Vector3.forward;
                float localSpeed = Speed;
                //Vector3.Normalize(moveDir)
                _controller.Move(moveDir.normalized * localSpeed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump") && _isGrounded)
                _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
            if (Input.GetButtonDown("Dash") & dashTimer >= 5)
            {
                // 
                dashTimer = 0;
                Debug.Log("Dash");
                _velocity += Vector3.Scale(moveDir.normalized, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
            }


            _velocity.y += Gravity * Time.deltaTime;

            _velocity.x /= 1 + Drag.x * Time.deltaTime;
            _velocity.y /= 1 + Drag.y * Time.deltaTime;
            _velocity.z /= 1 + Drag.z * Time.deltaTime;

            _controller.Move(_velocity * Time.deltaTime);
        }

    }
    private void FixedUpdate() {
     if (Input.GetKey(KeyCode.Escape)){
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }   
    }}
