using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using WaterStylizedShader;

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
    public float massMultiplier;

    [Range(0, 2)] public float scaleSpeed = 1f;

    [SerializeField]
    private GameObject playerRef;
    private DuckieMovement duckster;
    private FloatingObject floatingObj;
    [SerializeField]
    private float duckFloatPower;
    private float origDuckFloatPower;
    [SerializeField]
    private CinemachineThirdPersonFollow duckCam;
    private float initialCamDistance;
    [Range(0, 100)]
    public float bigDuckCamDistance;

    private bool isActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameManager.instance.player;
        duckCam = GameManager.instance.duckcam;
        gameObject.tag = "PowerupBig";
        duckster = playerRef.GetComponent<DuckieMovement>();
        floatingObj = playerRef.GetComponent<FloatingObject>();
        origDuckFloatPower = floatingObj.floatingPower;
        duckFloatPower = floatingObj.floatingPower;

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
        if (other.CompareTag("Player") && !isActive)
        {
            Debug.Log("Player collided");
            Debug.Log("Is Active is " + isActive);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;

            isActive = true;
            ScoreManager.instance.AddScore(100);            

            Vector3 targetScale = new Vector3(duckScaleX, duckScaleY, duckScaleZ);

            StartCoroutine(LerpOverTime(targetScale, bigDuckCamDistance));
            
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

        duckster.GetComponent<Rigidbody>().mass = (duckScaleX + duckScaleY + duckScaleZ) * massMultiplier;
        duckster.Speed = duckSpeed;
        floatingObj.floatingPower = duckFloatPower;
        duckCam.CameraDistance = bigDuckCamDistance;

        Debug.Log("Lerp Coroutine complete");
    }

    private IEnumerator ResetDuckSize()
    {
        yield return new WaitForSeconds(resetDelay);

        Debug.Log("Duck size reset initialized");

        StartCoroutine(LerpOverTime(initialScale, initialCamDistance));

        duckster.GetComponent<Rigidbody>().mass = initialMass;
        duckster.Speed = initialSpeed;
        floatingObj.floatingPower = origDuckFloatPower;
        isActive = false;
        
        yield return new WaitForSeconds(2);
        
        Debug.Log("Waited and self-destructed");

        Destroy(gameObject);
    }
}
