using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCheck : MonoBehaviour
{
    public test_level level;
    public GameObject scene_object;

    public int coins_left;

    // Start is called before the first frame update
    void Start()
    {
        scene_object = GameObject.Find("test_level");
        level = scene_object.GetComponent<test_level>();
    }

    // Update is called once per frame
    void Update()
    {
        coins_left = level.coins;        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(coins_left == null)
        {
            Debug.Log("Coins left is null");
        }
        if (coins_left == 0)
        {
            Debug.Log("Entered Door");
            SceneManager.LoadScene("test_level2");
        }
        else
        {
            Debug.Log($"{coins_left} coins left");
        }
        
    }

    public void OnTriggerExit2D(Collider2D col)
    {
   
    }
}
