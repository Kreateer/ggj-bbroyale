using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private DuckieMovement duckster;

    private Vector3 theForceDirection;
    [SerializeField]
    private float theForceX;
    [SerializeField]
    private float theForceY;

    private Rigidbody rigidDuck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.tag = "SplashTrap";
        duckster = player.GetComponent<DuckieMovement>();
        rigidDuck = player.GetComponent<Rigidbody>();
        theForceDirection = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        theForceDirection = new Vector3(theForceX, theForceY, 0);
        if (other.CompareTag("Player"))
        {
            Debug.Log("SPLASH AAAAAAA");
            rigidDuck.AddForce(theForceDirection, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
