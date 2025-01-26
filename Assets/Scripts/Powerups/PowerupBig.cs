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

    [Range(0, 2)] public float scaleSpeed = 1f;

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
        playerRef = GameManager.instance.player;
        duckCam = GameManager.instance.duckcam;
        gameObject.tag = "PowerupBig";
        duckster = playerRef.GetComponent<DuckieMovement>();

        initialY = transform.position.y;
        initialScale = transform.localScale;
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
            Vector3 targetScale = new Vector3(duckScaleX, duckScaleY, duckScaleZ);
            StartCoroutine(LerpOverTime(targetScale, bigDuckCamDistance));

            duckster.GetComponent<Rigidbody>().mass = (duckScaleX + duckScaleY + duckScaleZ) * 2;
            duckster.Speed = duckSpeed;
            ScoreManager.instance.AddScore(100);
            duckCam.CameraDistance = bigDuckCamDistance;
            
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            
            StartCoroutine(ResetDuckSize());
        }
    }

    private IEnumerator LerpOverTime(Vector3 targetScale, float targetCamDistance)
    {
        float elapsedTime = 0f;
        Vector3 startScale = playerRef.transform.localScale;
        float startCamDistance = duckCam.CameraDistance;

        while (elapsedTime < scaleSpeed)
        {
            // Lerp the scale of the duck
            playerRef.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / scaleSpeed);

            // Lerp the camera distance
            duckCam.CameraDistance = Mathf.Lerp(startCamDistance, targetCamDistance, elapsedTime / scaleSpeed);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure final values are exactly at the targets
        playerRef.transform.localScale = targetScale;
        duckCam.CameraDistance = targetCamDistance;
    }

    private IEnumerator ResetDuckSize()
    {
        yield return new WaitForSeconds(resetDelay);

        StartCoroutine(LerpOverTime(initialScale, initialCamDistance));

        duckster.GetComponent<Rigidbody>().mass = initialMass;
        duckster.Speed = initialSpeed;

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
