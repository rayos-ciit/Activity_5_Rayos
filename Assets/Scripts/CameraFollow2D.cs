using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform player;
    private float offsetY;

    void Start()
    {
        if (player != null) offsetY = transform.position.y - player.position.y;
    }

    void Update()
    {
        if (player != null)
        {
            //follower player movement
            transform.position = new Vector3(0f, player.position.y + offsetY, -10f);
        }
    }
}