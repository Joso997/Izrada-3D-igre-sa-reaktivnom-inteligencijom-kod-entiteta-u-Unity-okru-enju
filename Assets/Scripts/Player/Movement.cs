using UnityEngine;
using System.Collections.Generic;
using System;

public class Movement : MonoBehaviour
{
    private Vector2 touchOrigin = -Vector2.one;
    private int horizontal;
    private int vertical;
    public float speed = 4.0F;
    private float jumpSpeed = 10.0F;
    private float gravity = 30.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private bool buttonpressed = false;
    public GameObject mobileUI;
    public VirtualJoystick joystick;
    public Dictionary<string, float> values;
    public bool jumpEnabled = true;
    public bool limitDiagonalSpeed = true;
    bool changemovement = false;
    int waitframes = 10;
    public delegate void MovementLoaded();
    public MovementLoaded movementLoaded;
    void Start()
    {
        Reset();
        controller = GetComponent<CharacterController>();
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        mobileUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>().Find("MobileUI").gameObject;
        joystick = mobileUI.transform.Find("BackgroundImage").gameObject.GetComponent<VirtualJoystick>();      
#endif
    }
    public void Reset()
    {
        values = new Dictionary<string, float>();
        values.Add("speed", speed);
        values.Add("jumpSpeed", jumpSpeed);
        values.Add("gravity", gravity);
        changemovement = false;
        if(movementLoaded != null)
            movementLoaded();
    }
    void Update()
    {
        if (controller.isGrounded)
        {
            move();
            if ((Input.GetButton("Jump") || buttonpressed) && jumpEnabled && !GetComponent<DiggingFunction>().belowground)
                moveDirection.y = values["jumpSpeed"];
        }
        if (changemovement)
            rotate();
        moveDirection.y -= values["gravity"] * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if (Input.GetButton("ChangeMovement") && !changemovement && waitframes >= 10)
        {
            changemovement = true;
            waitframes = 0;
        }else if (Input.GetButton("ChangeMovement") && changemovement && waitframes >= 10)
        {
            changemovement = false;
            waitframes = 0;
        }
        waitframes++;
        buttonpressed = false;
    }
    
    private float getHorizontal()
    {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        return joystick.Horizontal();
#else
        if (changemovement)
            return Input.GetAxis("Horizontal");
        else
            return Input.GetAxisRaw("Horizontal");
#endif

    }
    private float getVertical()
    {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        return joystick.Vertical();
#else
        if (changemovement)
            return Input.GetAxis("Vertical");
        else
            return Input.GetAxisRaw("Vertical");
#endif


    }
    private void move()
    {
        float mv_speed = values["speed"];
        float inputX = getHorizontal();
        float inputZ = getVertical();
        float inputModifyFactor = (inputX != 0.0f && inputZ != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;
        if (changemovement)
        {
            moveDirection = new Vector3(0, 0, getVertical());
            moveDirection = transform.TransformDirection(moveDirection);
            //Debug.Log(values["speed"]);
            moveDirection *= values["speed"];
        }else
        {
            moveDirection.x = inputX * (Input.GetButton("Jump") && jumpEnabled ? jumpSpeed / 2 : mv_speed) * inputModifyFactor;
            moveDirection.z = inputZ * (Input.GetButton("Jump") && jumpEnabled ? jumpSpeed / 2 : mv_speed) * inputModifyFactor;
            moveDirection.y = 0;
            if (inputX != 0 || inputZ != 0)
            {
                rotate();
            }
        }             
        
    }
    private void rotate()
    {
        if (changemovement)
            transform.Rotate(0, getHorizontal() * values["speed"], 0);        
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), values["speed"] * Time.deltaTime);

    }
    public void Jump()
    {
        buttonpressed = true;
    }
    public bool ChangeMovement()
    {
        changemovement = !changemovement;
        return changemovement;
    }
    public bool OnGround()
    {
        return controller.isGrounded;
    }
}