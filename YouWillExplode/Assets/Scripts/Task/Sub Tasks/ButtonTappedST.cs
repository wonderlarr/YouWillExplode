using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTappedST : BaseST, IPointerDownHandler
{
    // Component references
    private TextMeshProUGUI text;

    // Subtask Generics here

    [Space]
    [Header("Button Specifics")]
    public float clickValMax = 3f;
    public float clickVal = 0;

    [Space]
    public float decaySpeed = 1f;


    //private void Awake()
    //{
    //    text = GetComponentInChildren<TextMeshProUGUI>();
    //    //taskController = GetComponentInParent<TaskController>();

    //}

    // Start is called before the first frame update
    void Start()
    {
        //text.SetText("0");
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
        //text.SetText(clickVal.ToString());
            
        // check for completion
        if (clickVal >= clickValMax)
        {
            CompleteSubtask();
        }
    }

    void CompleteSubtask()
    {
        task.CompleteSubtask(subWeight);
        subComplete = true;
    }

    public void OnPointerDown(PointerEventData ped)
    {
        // possibly add control
        clickVal += 1;
    }
}
