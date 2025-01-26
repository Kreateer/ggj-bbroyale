using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PowerupBig : MonoBehaviour
{

    [Range(0,5)] public float speed;
    [Range(0,100)] public float bobbingRange;
    public float resetDelay;
    private float initialY;
    private Vector3 initialScale;
    private float initialMass;
    private float initialSpeed;
    private float duckSpeed;
    public float duckScaleX;
    public float duckScaleY;
    public float duckScaleZ;

    [SerializeField]
    private GameObject playerRef;
    private DuckieMovement duckster;
    [SerializeField]
    private CinemachineThirdPersonFollow duckCam;
    private float initialCamDistance;
    [Range(0, 100)]
    public float bigDuckCamDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialY = transform.position.y;
        initialScale = transform.localScale;
        
        
        gameObject.tag = "PowerupBig";
        duckster = playerRef.GetComponent<DuckieMovement>();
        initialMass = duckster.GetComponent<Rigidbody>().mass;
        initialSpeed = duckster.Speed;
        initialCamDistance = duckCam.CameraDistance;

    }

    // Update is called once per frame
    void Update()
    {
        float bob = Mathf.PingPong(Time.time * speed, 1) * bobbingRange;
        transform.position = new Vector3(transform.position.x, initialY + bob, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided");
            playerRef.transform.localScale = new Vector3(duckScaleX, duckScaleY, duckScaleZ);
            duckster.GetComponent<Rigidbody>().mass = (duckScaleX + duckScaleY + duckScaleZ) * 2;
            duckster.Speed = duckSpeed;
            duckCam.CameraDistance = bigDuckCamDistance;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(ResetDuckSize());
        }
    }

    IEnumerator ResetDuckSize()
    {
        yield return new WaitForSeconds(resetDelay);
        playerRef.transform.localScale = initialScale;
        duckster.GetComponent<Rigidbody>().mass = initialMass;
        duckster.Speed = initialSpeed;
        duckCam.CameraDistance = initialCamDistance;
        Destroy(gameObject);
    }
}
