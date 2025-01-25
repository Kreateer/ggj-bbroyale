using Unity.VisualScripting;
using UnityEngine;

public class ShowerTrap : MonoBehaviour
{
    [SerializeField]
    private GameObject playerRef;
    private DuckieMovement duckster;
    private float ducksterPower;

    [SerializeField]
    private bool followPlayer;

    //[SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;

    [SerializeField]
    private float followSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.tag = "ShowerTrap";
        duckster = playerRef.GetComponent<DuckieMovement>();
        ducksterPower = duckster.Speed;
        minY = gameObject.transform.position.y;
    }

    private void FollowPlayer(bool enabled)
    {
        if (enabled)
        {
            //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerRef.transform.position, 
            //    (followSpeed * Time.deltaTime));

            Vector3 currentPosition = gameObject.transform.position;
            Vector3 targetPosition = playerRef.transform.position;

            // Move towards the player's X and Z positions
            Vector3 newPosition = Vector3.MoveTowards(
                new Vector3(currentPosition.x, 0, currentPosition.z),
                new Vector3(targetPosition.x, 0, targetPosition.z),
                followSpeed * Time.deltaTime
            );

            // Clamp the Y value and apply it to the final position
            newPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

            gameObject.transform.position = newPosition;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            duckster.Speed = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            duckster.Speed = ducksterPower;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            FollowPlayer(followPlayer);
        }
    }
}
