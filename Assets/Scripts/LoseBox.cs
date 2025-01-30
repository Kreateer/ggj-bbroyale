using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Lose");
        }
            
    }
}
