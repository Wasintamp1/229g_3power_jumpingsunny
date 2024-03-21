using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue;
    public Text timerText;

    public float addTime;

    public GameObject timeOut;
    public bool isTimeOut = false;

    public bool isStart = false;
    public Collider startCollider;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                if (!isTimeOut)
                {
                    timeValue = 3f;
                    isTimeOut = true;
                    timeOut.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PointStart")
        {
            isStart = true;
        }

        if (other.tag == "AddTime")
        {
            timeValue += addTime;
        }

        if (other.tag == "PointEnd")
        {
            
        }
    }
}
