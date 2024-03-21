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

    public bool isEnd = false;
    public GameObject endCredit;

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
                timeValue -= Time.deltaTime; // count down time
            }
            else if (isEnd && timeValue < 0) // when video credit end back to main menu
            {
                SceneManager.LoadSceneAsync(0);
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
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reset scene
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

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // show text in canvas
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PointStart") // hit object start count down time
        {
            isStart = true;
        }

        if (other.tag == "AddTime") // hit Object add more time 
        {
            timeValue += addTime;
        }

        if (other.tag == "PointEnd") // hit object start play Endcredit
        {
            timeValue = 8f;
            isEnd = true;
            endCredit.SetActive(true);
            timerText.enabled = false;
        }
    }
}
