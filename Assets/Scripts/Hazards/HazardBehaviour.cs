using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    public Vector3 goal;
    public float speed;
    public bool wall = false;

    Vector3 direction;

    private void Start()
    {
        if(goal != null)
            direction = (goal - transform.position).normalized;
    }

    private void Update()
    {
        if(goal != null)
        {
            transform.position = transform.position + direction * (speed * Time.deltaTime);
        }
    }
}
