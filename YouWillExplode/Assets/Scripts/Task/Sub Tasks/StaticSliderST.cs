using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticSliderST : BaseST
{
    // Private vars
    private int clicked;

    // Component references
    //private TaskController taskController;
    private TextMeshProUGUI text;

    [Space]
    [Header("Slider Specifics")]
    public float sliderGoal;
    public float sliderLeniency = 5; // This is only one direction worth, doubled in practice.


    private void Awake()
    {
        //taskController = GetComponentInParent<TaskController>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        text.SetText("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (subComplete)
        {
            return;
        }

        // Update holdtime, clamped at 0 and max
/*        holdTime += Time.deltaTime * clicked;
        holdTime = Mathf.Clamp(holdTime, 0, holdTimeMax);

        // Update button text with current time
        text.SetText(holdTime.ToString());
            
        // check for completion
        if (holdTime >= holdTimeMax)
        {
            CompleteSubtask();
        }*/
    }

    void CompleteSubtask()
    {
        taskController.CompleteSubtask(subWeight);
        subComplete = true;
    }

    public void ClickDown()
    {
        clicked = 1;
    }

    public void ClickUp()
    {
        clicked = -1;
    }
}
