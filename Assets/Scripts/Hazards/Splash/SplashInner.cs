using System.Collections;
using UnityEngine;

public class SplashInner : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    private DuckieMovement duckster;

    private Vector3 theForceDirection;
    [SerializeField]
    private float theForceY;

    [SerializeField]
    private float ducksterMass;

    private float defaultMass;

    private Rigidbody rigidDuck;

    private ParticleSystem splashParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameManager.instance.player;
        gameObject.tag = "SplashTrapInner";
        duckster = Player.GetComponent<DuckieMovement>();
        rigidDuck = Player.GetComponent<Rigidbody>();
        splashParticle = GetComponentInParent<ParticleSystem>();
        theForceDirection = new Vector3(0.0f, 0.0f, 0.0f);
        defaultMass = rigidDuck.mass;

    }

    IEnumerator ResetDucklerMass()
    {
        Debug.Log("COROUTINE STARTED");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("RESETTING MASS");
        rigidDuck.mass = defaultMass;
    }

    private void OnTriggerEnter(Collider other)
    {
        theForceDirection = new Vector3(0, theForceY, 0);
        if (other.CompareTag("Player"))
        {
            Debug.Log("SPLASH AAAAAAA");
            rigidDuck.mass = ducksterMass;
            rigidDuck.AddForce(theForceDirection, ForceMode.Impulse);
            StartCoroutine(ResetDucklerMass());
        }
    }

    

    // Update is called once per frame
    void Update()
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
