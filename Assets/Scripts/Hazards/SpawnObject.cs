using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public static void Spawn(Vector3 origin, Vector3 goal, GameObject go, GameObject player)
    {
        if (go == null) return;

        GameObject aux = Instantiate(go, origin, Quaternion.identity);
        if (aux.GetComponent<Splash>()  != null)
        {
            aux.GetComponent<Splash>().Player = player;
        }
        if (aux.GetComponent<HazardBehaviour>() != null) 
        { 
            aux.GetComponent<HazardBehaviour>().goal = goal;
            aux.transform.LookAt(goal);
            if (aux.tag == "Wave")
                aux.transform.eulerAngles = new Vector3(aux.transform.eulerAngles.x, aux.transform.eulerAngles.y, 90);
        }
    }

}
