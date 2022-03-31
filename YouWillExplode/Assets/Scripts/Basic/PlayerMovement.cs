using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float walkMult = 1f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get axis input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(x, y, 0);

        // Move player
        transform.Translate(move * Time.deltaTime * walkMult);

    }
}
