using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    private CircleCollider2D rad;
    private SpriteRenderer sprite;

    // Awake is called during object construction
    void Awake()
    {
        // Get components
        rad = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Make a duplicate material, allowing for runtime changes that only effect THIS object.
        //sprite.material = new Material(sprite.material);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for a player in range, apply the outline if found.
        Debug.Log(sprite.material.HasInt("_ENABLED"));

    }

    // Our only trigger is the circle collider on the object itself, this will work for now.
    // If needed we can make a child object for this in the future.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.material.SetInt("_ENABLED", 1);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        sprite.material.SetInt("_ENABLED", 1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sprite.material.SetInt("_ENABLED", 0);
    }
}
