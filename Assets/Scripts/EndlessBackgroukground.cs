using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    
    [Header("Settings")]
    public float backgroundHeight = 10f;

    void Update()
    {
        //making sure its seamless
        if (cameraTransform.position.y - transform.position.y >= backgroundHeight)
        {
            //teleport previous photo
            transform.position = new Vector3(
                transform.position.x, 
                transform.position.y + (backgroundHeight * 2f), 
                transform.position.z
            );
        }
    }
}