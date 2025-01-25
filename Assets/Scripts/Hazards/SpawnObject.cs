using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public static void Spawn(Vector3 origin, Vector3 goal, GameObject go)
    {
        if (go == null) return;

        GameObject aux = Instantiate(go, origin, Quaternion.identity);
        aux.GetComponent<HazardBehaviour>().goal = goal;
    }

}
