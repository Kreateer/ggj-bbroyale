using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] gos;
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Vector3 origin = new Vector3(Random.Range(-100, 100), 0, Random.Range(50, 150));
            Vector3 goal = player.transform.position;
            SpawnObject.Spawn(origin, goal, gos[Random.Range(0,1)]);
        }
    }
}
