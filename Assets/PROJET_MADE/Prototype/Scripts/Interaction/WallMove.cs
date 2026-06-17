using UnityEngine;

public class WallMove : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.forward;
    public float speed = 2f;
    public float maxDistance = 10f;

    private Vector3 startPosition;
    private bool move = false;

    void Start()
    {
        startPosition = transform.position;
    }

    public void StartMoving()
    {
        move = true;
    }

    void Update()
    {
        if (!move) return;

        transform.position += moveDirection * speed * Time.deltaTime;

        float traveled = Vector3.Distance(startPosition, transform.position);

        if (traveled >= maxDistance)
        {
            move = false;
        }
    }
}