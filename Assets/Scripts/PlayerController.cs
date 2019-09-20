using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Reflection;
public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public CharacterController2D controller;

    public float runSpeed = 0.8f;

    public bool grounded = true;
    public bool last_grounded = true;

    bool jumping = false;

    bool dodging = false;

    public float dodgeSpeed;
    public float startDodgeTime;
    float dodgeTime=0;

    private Rigidbody2D rb;

    public Animator anim;

    float lastx=1;

    float lasty=1;

    public int player_number = 1;

    // attacking
    private float timeBtwAttack;

    public float startTimeBtwAttack;

    public Transform attackPos;

    public LayerMask whatIsEnemies;

    public float attackRange;

    public float damage;

    public Color color;



    void PlayerInit()
    {
        // player init
        if(PlayerState.player[player_number].dodgeSpeed == 0)
        {
            PlayerState.player[player_number].dodgeSpeed = dodgeSpeed;
        }
        else
        {
            dodgeSpeed = PlayerState.player[player_number].dodgeSpeed;
        }

        if(PlayerState.player[player_number].startDodgeTime == 0)
        {
            PlayerState.player[player_number].startDodgeTime = startDodgeTime;
        }
        else
        {
            startDodgeTime = PlayerState.player[player_number].startDodgeTime;
        }
    }
    
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

        Color c = new Color(60/100f, 60/100f, 1, 1);  
        player.GetComponent<SpriteRenderer>().color = c;
        //player.GetComponent<SpriteRenderer>().material.SetFloat("_Strength", 0.0001f);

        PlayerInit(); 
    }

    // Update is called once per frame
    void Update()
    {
        // set grounded
        anim.SetBool("grounded", grounded);

        // jump check
        if (Input.GetAxis("Jump_" + player_number)==1)
        {
            anim.Play("inAir");
            jumping = true;
        }
        if (!grounded)
        {
            anim.Play("inAir");
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
                anim.Play("dash");
            }
        }
        else if (Input.GetAxis("Dodge_" + player_number)==0)
        {
            dodging = false;
            anim.Play("walk");
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

        // process attacks
        if(timeBtwAttack <= 0)
        {
            if(Input.GetAxis("Attack_" + player_number) == 1)
            {
                Debug.Log("Trying to hit...");
                anim.Play("attack");
                Collider2D [] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToHit.Length; i++)
                {
                    Vector2 direction_hit = new Vector2(lastx, 0);
                    Debug.Log("HIT");
                    try
                    {
                        PlayerController p = enemiesToHit[i].GetComponent<PlayerController>();
                        if(p != this)
                        {
                            p.GetHit(direction_hit, 30f);    
                        }
                    }
                    catch(NullReferenceException e)
                    {
                        Debug.Log("failed to hit");
                    }
                    
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else{
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void FixedUpdate()
    {
        if(last_grounded != grounded){
            last_grounded = grounded;
        }    
        
    }

    public void GetHit(Vector2 direction, float force)
    {
        rb.AddForce(direction*force);
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
