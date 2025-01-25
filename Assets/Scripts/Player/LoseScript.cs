using UnityEngine;

public class LoseScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LoseBox")
        {
            Time.timeScale = 0f;
        }
    }
}
