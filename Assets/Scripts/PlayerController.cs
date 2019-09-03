using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    PlayerxControl controls;

    public GameObject player;
    public CharacterController2D controller;

    float horizontalMove = 0f;

    public float runSpeed = 0.0008f;

    public bool grounded=true;
    public bool last_grounded = true;

    bool jumping = false;

    bool dodging = false;

    private Rigidbody2D rb;

    private Animator anim;

    Vector2 move;

    public int scene = 0;

    void Awake ()
    {
        controls = new PlayerxControl();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => jumping = true;;
        controls.Gameplay.Jump.canceled += ctx => jumping = false;
        controls.Gameplay.Dash.performed += ctx => Dodge(true);
        controls.Gameplay.Dash.canceled += ctx => Dodge(false);
    }

    void Start()
    {
        player = GameObject.Find("guy");
        rb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // set grounded
        anim.SetBool("grounded", grounded);

        // Movement updates
        Vector2 corrected = move * Time.deltaTime;

        // jump check
        if (!grounded)
        {
            jumping = false;
        }

        // move the character
        controller.Move(move.x, false, jumping);
        anim.SetFloat("horizontalMove", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    void FixedUpdate()
    {
        if(last_grounded != grounded){
            Debug.Log($"Grounded = {grounded}");
            last_grounded = grounded;
        }    
        
    }

    void Dodge(bool dashing)
    {
        Debug.Log($"Dodged: not done, state: {dashing}");
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
