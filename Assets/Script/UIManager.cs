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
    public Text TimerText;
    public Text HintText;

    float saveTempTimer;
    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        saveTempTimer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset the timer, keyboard R
        if (Input.GetKeyDown("r"))
        {
            Timer = saveTempTimer;
            RefreshTimerText(TimeToString(Timer));
            isStart = false;
        }
        
        if (Input.GetKeyDown("s"))
        {
            isStart = true;
        }

        // Start timer, keyborad S
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
            }
        }


        // Quit
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
        string outputtime = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", hour, minute, second, millisecond);
        return outputtime;
    }

    public float getSetTimer()
    {
        return Timer;
    }
}
