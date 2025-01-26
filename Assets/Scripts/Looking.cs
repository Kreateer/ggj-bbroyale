using UnityEngine;

public class Looking : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        gameObject.transform.LookAt(player.transform.position);
    }
}
