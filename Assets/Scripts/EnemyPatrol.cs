using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float moveSpeed = 2f;
    public float moveDistance = 2f;

    Vector3 startingPosition;
    int direction = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;
        float distanceFromStart = transform.position.x - startingPosition.x;
        if (Mathf.Abs(distanceFromStart) >= moveDistance)
        {
            direction *= -1;
        }
    }
}
