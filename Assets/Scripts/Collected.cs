using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collected : MonoBehaviour
{
    public test_level level;
    public GameObject scene_object;

    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        scene_object = GameObject.Find("test_level");
        level = scene_object.GetComponent<test_level>();

        coin = GameObject.Find("coin");
        coin.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Coin collected");
        if(level.coins > 0)
        {
            level.coins -= 1;
            coin.SetActive(false);
        }
    }
}
