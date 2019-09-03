using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    PlayerxControl controls;

    bool start = false;

    bool settings = false;

    bool quit = false;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerxControl();
        controls.Gameplay.Jump.performed += ctx => start=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("test_level");
        }
        Debug.Log($"Start = {start}");
        if(start)
        {
            SceneManager.LoadScene("test_level");
        }
    }
}
