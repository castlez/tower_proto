using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Reflection;
public class PlayerController : MonoBehaviour
{

    PlayerxControl controls;

    public GameObject player;
    public CharacterController2D controller;

    float horizontalMove = 0f;

    public float runSpeed = 0.8f;

    public bool grounded = true;
    public bool last_grounded = true;

    bool jumping = false;

    bool dodging = false;

    public float dodgeSpeed;
    public float startDodgeTime;
    float dodgeTime=0;

    private Rigidbody2D rb;

    private Animator anim;

    float lastx=1;

    float lasty=1;

    public int scene = 0;

    public int player_number = 1;
    
    void Awake ()
    {
        // Get Player State
        // Type t = Type.GetType("PlayerState" + player_number);
        // var State = Activator.CreateInstance(t);


        if(PlayerState.player[player_number].active)
        {
            Debug.Log($"Player {player_number} is active!");
        }
    }

    void Start()
    {
        player = GameObject.Find("guy" + player_number);
        rb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // set grounded
        anim.SetBool("grounded", grounded);

        // jump check
        if (Input.GetAxis("Jump_" + player_number)==1)
        {
            jumping = true;
        }
        if (!grounded)
        {
            jumping = false;
        }

        // dash check
        if(dodgeTime>0)
        {
            dodgeTime -= Time.deltaTime;
        }
        if (!dodging & Input.GetAxis("Dodge_" + player_number)==1)
        {
            dodging = true;
            if(dodgeTime<=0)
            {
                dodgeTime = startDodgeTime;
                Vector2 d = new Vector2(lastx, 0);
                rb.AddForce(d * dodgeSpeed);
            }
        }
        else if (Input.GetAxis("Dodge_" + player_number)==0)
        {
            dodging = false;
        }
        
        // move the character
        float movex = Input.GetAxis("Horiz_" + player_number) * runSpeed * Time.deltaTime;
        if(movex != 0)
        lastx = movex;
        float movey = Input.GetAxis("Vert_" + player_number) * runSpeed * Time.deltaTime;
        if(movey != 0)
        lasty = movey;
        controller.Move(movex, false, jumping);
        anim.SetFloat("horizontalMove", Mathf.Abs(movex));
    }

    void FixedUpdate()
    {
        if(last_grounded != grounded){
            last_grounded = grounded;
        }    
        
    }

    void OnEnable()
    {
        // controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        // controls.Gameplay.Disable();
    }
}
