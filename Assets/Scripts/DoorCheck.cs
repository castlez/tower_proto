using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCheck : MonoBehaviour
{
    public Level level;
    public GameObject scene_object;

    public bool condition_met;

    public string next_screen;

    // Start is called before the first frame update
    void Start()
    {
        //scene_object = GameObject.Find(next_screen);
        scene_object = transform.parent.gameObject;
        level = scene_object.GetComponent<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!condition_met)
        condition_met = level.complete;        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(condition_met == null)
        {
            Debug.Log("Error has occured, completed is null");
        }
        if (condition_met == true)
        {
            Debug.Log($"Objective Completed: transfering to scene: {next_screen}");
            SceneManager.LoadScene(next_screen);
        }
        else
        {
            Debug.Log("Objective not yet completed!");
        }
        
    }

    public void OnTriggerExit2D(Collider2D col)
    {
   
    }
}
