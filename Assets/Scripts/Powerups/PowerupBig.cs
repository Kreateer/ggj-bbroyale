using UnityEngine;

public class PowerupBig : MonoBehaviour
{

    [Range(0,5)] public float speed;
    [Range(0,100)] public float bobbingRange;

    private float initialY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float bob = Mathf.PingPong(Time.time * speed, 1) * bobbingRange;
        transform.position = new Vector3(transform.position.x, initialY + bob, transform.position.z);
    }
}
