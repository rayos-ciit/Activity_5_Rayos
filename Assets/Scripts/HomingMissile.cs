using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public float speed = 8f;
    public float turnSpeed = 4f;
    public float lifeTime = 5f;
    public float hitRadius = 1.0f; // sub for collision
    
    private float timer = 0f;

    void Update()
    {
        //check for how long it's been alive
        timer += Time.deltaTime;
        if (timer >= lifeTime) 
        {
            Destroy(gameObject);
            return;
        }

        if (target != null)
        {
            // "collision"
            if (Vector2.Distance(transform.position, target.position) <= hitRadius)
            {
                GameManager.Instance.TakeDamage();
                Destroy(gameObject);
                return;
            }

            
            Vector3 direction = (target.position - transform.position).normalized;
            
            //force Z forward
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            
            //slerp towards target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}