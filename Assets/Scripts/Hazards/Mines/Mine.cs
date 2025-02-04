using UnityEngine;

public class Mine : MonoBehaviour
{

    private ParticleSystem ParticleExplosion;
    private float particleDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the tag of this GameObject to Mine
        gameObject.tag = "Mine";
        ParticleExplosion = GetComponentInChildren<ParticleSystem>();
        particleDuration = ParticleExplosion.main.duration + ParticleExplosion.main.startLifetime.constantMin;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the collider of the other GameObject involved in the collision is tagged "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered by Player");
            ParticleExplosion.Play(true);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            ScoreManager.instance.AddScore(-15);
            Vector3 theForceDirection = (other.transform.position - transform.position).normalized * 150;
            other.GetComponent<Rigidbody>().AddForce(theForceDirection, ForceMode.Impulse);

            Destroy(gameObject, particleDuration);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
