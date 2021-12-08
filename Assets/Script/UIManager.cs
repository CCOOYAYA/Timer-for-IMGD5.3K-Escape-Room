using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager S;
    private void Awake()
    {
        S = this;
    }

    
    

    public float Timer;        //total seconds
    public int timeToStop;
    public Text TimerText;
    public Text HintText;
    public AudioSource OutOfTime;
    public GameObject canvas;
    public GameObject videoPlayer;

    float saveTempTimer;
    public float maxTime;
    bool isStart = false;
    bool isOutOfTime = false;
    int num = 0;
    bool isVideoActive = false;

    // Start is called before the first frame update
    void Start()
    {
        RefreshTimerText(TimeToString(Timer));
        canvas.SetActive(false);
        videoPlayer.SetActive(false);
        saveTempTimer = Timer;
        maxTime = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset the timer, keyboard R or mouse right click
        if (Input.GetKeyDown("r") || Input.GetMouseButton(1))
        {
            Timer = saveTempTimer;
            RefreshTimerText(TimeToString(Timer));
            canvas.SetActive(false);
            videoPlayer.SetActive(false);
            isStart = false;
        }

        // Start timer, keyborad S or mouse left click
        if (Input.GetKeyDown("s") || Input.GetMouseButton(0))
        {
            if (!canvas.active && !videoPlayer.active)
            {
                isStart = true;
            }
            
        }
        
        // Pause timer, keyboard P
        if (Input.GetKeyDown("p"))
        {
            isStart = !isStart;
        }

        // Play the video, keyboard SPACE
        // Auto stops when video ends
        if (Input.GetKeyDown("space"))
        {
            if (!canvas.active && !videoPlayer.active)
            {
                isStart = false;
                StopAllCoroutines();
                canvas.SetActive(true);
                videoPlayer.SetActive(true);
                // Stop playing video when ends
                StartCoroutine("StopPlayVideo");
                // Continue the timer after video ends
                StartCoroutine("ContinueTimer");
            }   
        }
        
        // add 1 minute, keyboard =
        if (Input.GetKeyDown("="))
        {
            Timer += 60;
            maxTime += 60;
        }

        // decrease 1 minute, keyboard -
        if (Input.GetKeyDown("-"))
        {
            if (Timer >= 60)
            {
                Timer -= 60;
                maxTime -= 60;
            }
        }
        
        if (isStart)
        {
            if (Timer >= 0)
            {
                Timer -= Time.deltaTime;
                RefreshTimerText(TimeToString(Timer));
            }
            else
            {
                RefreshTimerText(TimeToString(0f));
                isOutOfTime = true;
                if (!OutOfTime.isPlaying && num < 3)
                {
                    OutOfTime.Play();
                    num++;
                }
            }
        }


        // Quit timer
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

    }


    private void RefreshTimerText(string str)
    {
        TimerText.text = str;
    }

    public string TimeToString(float setTimer)
    {
        int hour = (int)setTimer / 3600;
        int minute = ((int)setTimer - hour * 3600) / 60;
        int second = (int)setTimer - hour * 3600 - minute * 60;
        int millisecond = (int)((setTimer - (int)setTimer) * 1000);
        string outputtime = string.Format("{0:D2}:{1:D2}", minute, second);
        return outputtime;
    }

    public float getSetTimer()
    {
        return Timer;
    }

    public float updateMaxTime()
    {
        return maxTime;
    }

    IEnumerator ContinueTimer()
    {
        yield return new WaitForSeconds(timeToStop);
        isStart = true;
        yield return null;
    }

    IEnumerator StopPlayVideo()
    {
        yield return new WaitForSeconds(timeToStop);
        canvas.SetActive(false);
        videoPlayer.SetActive(false);
        yield return null;
    }
}
