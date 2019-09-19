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

    private Rigidbody2D rb;

    private Animator anim;

    public float movex = 0;

    public int scene = 0;

    public int player_number = 1;
    
    void Awake ()
    {
        // Get Player State
        // Type t = Type.GetType("PlayerState" + player_number);
        // var State = Activator.CreateInstance(t);


        if(PlayerState.state[player_number].active)
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

        float i = Input.GetAxis("Jump_" + player_number);
        if (Input.GetAxis("Jump_" + player_number)==1)
        {
            jumping = true;
        }
        if (!grounded)
        {
            jumping = false;
        }


        // move the character
        movex = Input.GetAxis("Horiz_" + player_number) * runSpeed * Time.deltaTime;
        controller.Move(movex, false, jumping);
        anim.SetFloat("horizontalMove", Mathf.Abs(movex));
    }

    void FixedUpdate()
    {
        if(last_grounded != grounded){
            last_grounded = grounded;
        }    
        
    }

    void Dodge(bool dashing)
    {
        Debug.Log($"Dodged: not done, state: {dashing}");
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
