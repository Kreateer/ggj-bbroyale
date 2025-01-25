using System.Collections;
using UnityEngine;
using WaterStylizedShader;

public class HazardBehaviour : MonoBehaviour
{
    public Vector3 goal;
    public float speed;
    public bool wall = false;

    Vector3 direction;

    bool flip = false, drag = false;
    GameObject aux = null;
    private void Start()
    {
        if(goal != null)
            direction = (goal - transform.position).normalized;
    }

    float faux = 0, timer = 0;
    private void Update()
    {
        if(goal != null)
        {
            transform.position = transform.position + direction * (speed * Time.deltaTime);
            if (flip)
            {
                if (aux.GetComponent<DuckieMovement>().enabled)
                {
                    aux.GetComponent<DuckieMovement>().enabled = false;
                    aux.GetComponent<FloatingObject>().enabled = false;
                    aux.transform.eulerAngles = Vector3.zero;
                }
                faux += transform.eulerAngles.z + Time.deltaTime * 100;
                aux.transform.eulerAngles = new Vector3(0, 0, faux);
                //aux.transform.eulerAngles += new Vector3(0, 0, transform.eulerAngles.z + Time.deltaTime * 100);
                //print(aux.transform.eulerAngles.z);
                if(aux.transform.eulerAngles.z > 345 && aux.transform.eulerAngles.z > 355)
                {
                    flip = false;
                    aux.GetComponent<DuckieMovement>().enabled = true;
                    aux.GetComponent<FloatingObject>().enabled = true;
                }
            }
            if(drag)
            {
                if (aux.GetComponent<DuckieMovement>().enabled)
                {
                    aux.GetComponent<DuckieMovement>().enabled = false;
                    aux.GetComponent<FloatingObject>().enabled = false;
                    aux.transform.eulerAngles = Vector3.zero;
                }
                aux.transform.position = transform.position;
                timer += Time.deltaTime;
                if(timer > 2)
                {
                    drag = false;
                    aux.GetComponent<DuckieMovement>().enabled = true;
                    aux.GetComponent<FloatingObject>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            aux = other.gameObject;
            switch(gameObject.tag)
            {
                case "Wave":
                    if (other.gameObject.GetComponent<DuckieMovement>().enabled) 
                    { 
                        float hitangle = Mathf.Abs(aux.transform.eulerAngles.y - transform.eulerAngles.y);
                        print(hitangle);
                        if (hitangle < 125 && hitangle > 55)
                            flip = true;
                        else if (hitangle < 55)
                        {
                            print(hitangle + " Boost");
                            StartCoroutine(BoostWave());
                        }
                        else if (hitangle > 125)
                        {
                            print(hitangle + " Jump");
                            float speed = Mathf.Abs(aux.GetComponent<Rigidbody>().linearVelocity.x + aux.GetComponent<Rigidbody>().linearVelocity.z);
                            aux.GetComponent<Rigidbody>().AddForce(aux.transform.up * speed * 20, ForceMode.Impulse);
                        }
                    }
                    break;
                case "Foam":
                    drag = true;
                    break;
            }
        }
    }

    IEnumerator BoostWave()
    {
        aux.GetComponent<DuckieMovement>().Power = aux.GetComponent<DuckieMovement>().Power * 2;
        yield return new WaitForSeconds(1f);
        aux.GetComponent<DuckieMovement>().Power = aux.GetComponent<DuckieMovement>().Power / 2;
        yield return null;
    }

}
