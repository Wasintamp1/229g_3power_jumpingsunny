using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float seconds = 60;
    //public float miliSeconds = 60;
    public int minutes = 1;
    public bool timers;

    private void Awake()
    {
        //player = GetComponent<Respawn>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (timers == true)
        {
            if (minutes > 0 || seconds > 0)
            {
                countDown();
            }
            else
            {
                timers = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (seconds == 0)
        {
            Debug.Log($"{minutes} : 00");
        }
        else
        Debug.Log($"{minutes} : {(int)Math.Round(seconds)}");


    }

    private void countDown()
    {
        if (seconds > 0)
        {
            seconds -= Time.deltaTime;
        }
        else
        {
            minutes -= 1;
            seconds += 59;
        }
    }
}
