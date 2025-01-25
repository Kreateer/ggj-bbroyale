using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] gos;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 origin = new Vector3(0, 0, 500);
            Vector3 goal = new Vector3(1, 1, 1);
            SpawnObject.Spawn(origin, goal, gos[0]);
        }
    }
}
