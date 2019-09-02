using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private GameObject player;
    private PlayerController pc;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("guy");
        pc = player.GetComponentInParent<PlayerController>();
        anim = player.GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        pc.grounded = true;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        pc.grounded = false;
    }
}
