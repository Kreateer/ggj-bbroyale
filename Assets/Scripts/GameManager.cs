using UnityEngine;
using static UnityEngine.UI.Image;

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
            Vector3 origin = new Vector3(Random.Range(-110, 110), -50, Random.Range(-55, 55));
            Vector3 goal = player.transform.position;
            SpawnObject.Spawn(origin, goal, gos[Random.Range(0,1)]);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            float randX = Random.Range(0, 150);
            float randZ = Random.Range(0, 80);
            if (randX < 120)
                randZ = Random.Range(60, 80);
            randX *= Random.Range(0, 2) * 2 - 1;
            randZ *= Random.Range(0, 2) * 2 - 1;
            //Vector3 origin = new Vector3(Random.Range(120, 150) * (Random.Range(0,2)*2-1), -50, Random.Range(60, 80) * (Random.Range(0, 2) * 2 - 1));
            Vector3 origin = new Vector3(randX, -50, randZ);
            Vector3 goal = player.transform.position;
            SpawnObject.Spawn(origin, goal, gos[Random.Range(1, 3)]);
        }
    }
}
