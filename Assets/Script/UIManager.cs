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
    bool isStart = false;
    bool isOutOfTime = false;
    bool isSoundPlayed = false;
    bool isVideoActive = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        videoPlayer.SetActive(false);
        saveTempTimer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset the timer, keyboard R or mouse right click
        if (Input.GetKeyDown("r") || Input.GetMouseButton(1))
        {
            Timer = saveTempTimer;
            RefreshTimerText(TimeToString(Timer));
            isStart = false;
        }

        // Start timer, keyborad S or mouse left click
        if (Input.GetKeyDown("s") || Input.GetMouseButton(0))
        {
            isStart = true;
        }

        // Play the video, keyboard SPACE
        // Auto stops when video ends
        if (Input.GetKeyDown("space"))
        {
            canvas.SetActive(true);
            videoPlayer.SetActive(true);
            Destroy(canvas, timeToStop);
            Destroy(videoPlayer, timeToStop);
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
                if (!OutOfTime.isPlaying && isSoundPlayed == false)
                {
                    OutOfTime.Play();
                    isSoundPlayed = true;
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
}
