using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTappedST : MonoBehaviour, IPointerDownHandler
{
    // Private vars
    //private float clickValue;

    // Component references
    private TaskController taskController;
    private TextMeshProUGUI text;
    private EventTrigger evSys;

    // Public vars
    [Header("Subtask Generics")]
    public bool subComplete = false;
    public int subWeight = 1;

    [Space]
    [Header("Button Specifics")]
    public float clickValMax = 3f;
    public float clickVal = 0;
    [Space]
    public float decaySpeed = 1f;


    private void Awake()
    {
        taskController = GetComponentInParent<TaskController>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        evSys = GetComponent<EventTrigger>();
        
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

        // Update clickVal, decreases over time.
        clickVal += -Time.deltaTime * decaySpeed; // Time.deltaTime is negative here!
        clickVal = Mathf.Clamp(clickVal, 0, clickValMax);

        // Update button text with current value
        text.SetText(clickVal.ToString());
            
        // check for completion
        if (clickVal >= clickValMax)
        {
            CompleteSubtask();
        }
    }

    void CompleteSubtask()
    {
        taskController.CompleteSubtask(subWeight);
        subComplete = true;
    }

    public void OnPointerDown(PointerEventData ped)
    {
        // possibly add control
        clickVal += 1;
    }
}
