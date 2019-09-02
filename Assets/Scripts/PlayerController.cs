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

    bool jumping = false;

    bool dodging = false;

    private Rigidbody2D rb;

    private Animator anim;

    Vector2 move;

    void Awake ()
    {
        controls = new PlayerxControl();
        controls.Gameplay.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => Jump(true, move);
        controls.Gameplay.Jump.canceled += ctx => Jump(false, move);
        controls.Gameplay.Dash.performed += ctx => Dodge(true);
        controls.Gameplay.Dash.performed += ctx => Dodge(false);
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
        
    }

    void FixedUpdate()
    {
        
    }

    void Move(Vector2 m)
    {
        Debug.Log($"move = {m.x}");
        Vector2 corrected = m * Time.deltaTime;
        // controller.Move(runSpeed + m.x * Time.fixedDeltaTime, false, jump);
        controller.Move(m.x, false, jumping);
        anim.SetFloat("horizontalMove", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    void Jump(bool jumping, Vector2 m)
    {
        this.jumping = jumping;
        controller.Move(m.x, false, jumping);
        anim.SetBool("grounded", grounded);

    }

    void Dodge(bool dashing)
    {
        Debug.Log("Dodged: NOT IMPLEMENTED");
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
