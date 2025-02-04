using System.Collections;
using UnityEngine;
using WaterStylizedShader;

public class HazardBehaviour : MonoBehaviour
{
    public Vector3 goal;
    public float speed, lifetime;
    public bool wall = false;

    Vector3 direction;

    bool flip = false, drag = false;
    GameObject aux = null;
    private void Start()
    {
        if(goal != null) 
        {
            Vector3 auxgoal = goal;
            if(goal.y > -50)
                auxgoal = new Vector3(goal.x, -50, goal.z);
            direction = (auxgoal - transform.position).normalized;
        }
    }

    float faux = 0, timer = 0, deathTime = 0;
    private void Update()
    {
        deathTime += Time.deltaTime;
        if (deathTime > lifetime)
            Destroy(gameObject);
        if(goal != null)
        {
            transform.position = transform.position + direction * (speed * Time.deltaTime);
            if (flip)
            {
                if (aux.GetComponent<DuckieMovement>().enabled)
                {
                    aux.GetComponent<DuckieMovement>().enabled = false;
                    aux.GetComponent<FloatingObject>().enabled = false;
                    aux.transform.eulerAngles = new Vector3(0, aux.transform.eulerAngles.y, 0);
                    aux.GetComponent<Rigidbody>().mass = aux.GetComponent<Rigidbody>().mass * 10;
                }
                faux += transform.eulerAngles.z + Time.deltaTime * 100;
                aux.transform.eulerAngles = new Vector3(aux.transform.eulerAngles.x, aux.transform.eulerAngles.y, faux);
                //aux.transform.eulerAngles += new Vector3(0, 0, transform.eulerAngles.z + Time.deltaTime * 100);
                //print(aux.transform.eulerAngles.z);
                if(aux.transform.eulerAngles.z > 350)
                {
                    flip = false;
                    aux.GetComponent<DuckieMovement>().enabled = true;
                    aux.GetComponent<FloatingObject>().enabled = true;
                    aux.GetComponent<Rigidbody>().mass = aux.GetComponent<Rigidbody>().mass / 10;
                }
            }
            if(drag)
            {
                if (aux.GetComponent<DuckieMovement>().enabled)
                {
                    aux.GetComponent<DuckieMovement>().enabled = false;
                    aux.GetComponent<FloatingObject>().enabled = false;
                    aux.transform.eulerAngles = new Vector3(aux.transform.eulerAngles.x, 0, 0);
                }
                aux.transform.position = transform.position;
                timer += Time.deltaTime;
                if(timer > 2 || aux.transform.position.x > 100 || aux.transform.position.x < -100 || aux.transform.position.z > 50 || aux.transform.position.z < -50)
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
                    if (other.gameObject.GetComponent<DuckieMovement>().enabled && !flip) 
                    { 
                        float hitangle = Mathf.Abs(aux.transform.eulerAngles.y - transform.eulerAngles.y);
                        print(hitangle);
                        if (hitangle < 125 && hitangle > 55)
                        {
                            ScoreManager.instance.AddScore(-15);
                            flip = true;
                        }
                        else if (hitangle < 55)
                        {
                            print(hitangle + " Boost");
                            StartCoroutine(Boost(2f, 2f));
                            ScoreManager.instance.AddScore(5);
                        }
                        else if (hitangle > 125)
                        {
                            print(hitangle + " Jump");
                            ScoreManager.instance.AddScore(50);
                            float speed = Mathf.Abs(aux.GetComponent<Rigidbody>().linearVelocity.x + aux.GetComponent<Rigidbody>().linearVelocity.z);
                            if(speed > 4)
                                aux.GetComponent<Rigidbody>().AddForce(aux.transform.up * speed * 50, ForceMode.Impulse);
                        }
                    }
                    break;
                case "Foam":
                    Vector3 vaux3 = aux.transform.position - transform.position;
                    float dist = Mathf.Abs(vaux3.x) + Mathf.Abs(vaux3.z);
                    if (dist < 6)
                    {
                        ScoreManager.instance.AddScore(-10);
                        drag = true;
                    }
                    else
                        StartCoroutine(Boost(2f, .5f));
                    break;
                case "BigBubble":
                    ScoreManager.instance.AddScore(20);
                    StartCoroutine(Boost(6f,4f));
                    Destroy(gameObject);
                    break;
            }
        }
    }

    IEnumerator Boost(float time, float force)
    {
        if (aux.GetComponent<DuckieMovement>().Speed < 11)
        {
            aux.GetComponent<DuckieMovement>().Speed = aux.GetComponent<DuckieMovement>().Speed * force;
            yield return new WaitForSeconds(time);
            if (gameObject.tag != "Foam")
                aux.GetComponent<DuckieMovement>().Speed = aux.GetComponent<DuckieMovement>().Speed / force;
        }
        yield return null;
    }

}
