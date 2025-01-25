using UnityEngine;

public class ShowerTrap : MonoBehaviour
{
    [SerializeField]
    private GameObject playerRef;
    private DuckieMovement duckster;
    private float ducksterPower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.tag = "ShowerTrap";
        duckster = playerRef.GetComponent<DuckieMovement>();
        ducksterPower = duckster.Power;
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
            duckster.Power = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            duckster.Power = ducksterPower;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
