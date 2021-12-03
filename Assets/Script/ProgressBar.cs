using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{

    float max;
    float current;
    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        max = UIManager.S.getSetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        current = UIManager.S.getSetTimer();
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)max;
        mask.fillAmount = fillAmount;
    }
}
