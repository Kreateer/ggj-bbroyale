using UnityEngine;

public class Splash : MonoBehaviour
{

    public GameObject Player;

    public bool bathring = false;

    private DuckieMovement duckster;

    private Vector3 theForceDirection;
    [SerializeField]
    private float theForceX;
    [SerializeField]
    private float theForceY;

    private Rigidbody rigidDuck;

    private ParticleSystem splashParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.tag = "SplashTrap";
        duckster = Player.GetComponent<DuckieMovement>();
        rigidDuck = Player.GetComponent<Rigidbody>();
        splashParticle = GetComponent<ParticleSystem>();
        theForceDirection = new Vector3(0.0f, 0.0f, 0.0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        theForceDirection = new Vector3(theForceX, theForceY, 0);
        if (bathring)
        {
            Vector3 center = new Vector3(0, -50, 0);
            theForceDirection = (center - transform.position).normalized * 100;
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("SPLASH AAAAAAA");
            rigidDuck.AddForce(theForceDirection * 10, ForceMode.Impulse);
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        theForceDirection = new Vector3(theForceX, theForceY, 0);
        if (other.CompareTag("Player"))
        {
            Debug.Log("STILL SPLASHING BOI");
            rigidDuck.AddForce(theForceDirection, ForceMode.Impulse);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if(splashParticle != null)
        {
            if (splashParticle.isPlaying)
            {
                gameObject.GetComponent<SphereCollider>().enabled = true;
            }
            if (splashParticle.isStopped)
            {
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }
}
