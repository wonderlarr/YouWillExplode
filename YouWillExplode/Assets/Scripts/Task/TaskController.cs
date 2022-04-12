using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    public bool tComplete;

    [Header("Task Score Config")]
    public int tScoreMax; // Completion value
    public int tScoreMin; // Starting value usually and hopefully
    public int tScore; // Current value

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CompleteTask()
    {
        Debug.Log("Task complete! Exploding...");
        Destroy(gameObject);

    }

    public void CompleteSubtask(int weight = 1)
    {
        tScore += weight;
        
        // Check if task is complete
        if (tScore >= tScoreMax)
        {
            tComplete = true;
            CompleteTask();
        }

        Debug.Log("Subtask Complete, weight " + weight.ToString());
    }
}
