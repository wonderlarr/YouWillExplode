using UnityEngine;

[RequireComponent(typeof(Camera))]
// Cannot be added unless it's a camera
public class Perportho : MonoBehaviour
{
   
    // Use Awake() instead of Start(), Awake happens during construction, Start happens on frame 1. Seems like a better idea.
    private void Awake()
    {
        // Sets a perspective camera to use Orthographic sorting, basically allowing for easier parallax without stupid workarounds
        // that I hate and hate and hate. It surprises me that at this point in game development, there isn't a better way to do
        // parallax using an orthographic camera than moving the background layers in a convincing way around the player.
        
        // 3 AM nonsensical rant over.

        GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;
    }
}
