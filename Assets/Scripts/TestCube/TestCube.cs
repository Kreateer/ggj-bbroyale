using UnityEngine;

public class TestCube : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(gameObject.GetType().Name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("A pressed");
            gameObject.transform.Translate(Vector3.left * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("D pressed");
            gameObject.transform.Translate(Vector3.right * Time.deltaTime);
        }
    }
}
