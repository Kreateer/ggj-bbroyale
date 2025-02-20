using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    public GameObject[] gos;
    public GameObject player;
    public CinemachineThirdPersonFollow duckcam;
    public Whirlpool wp;
    public Image speedI;
    public TextMeshProUGUI timerText, scoreText;
    public static GameManager instance;

    void Start()
    {
        instance = this;
    }

    bool onceB = true, onceW = true, onceM = true, onceS = true, onceP = true;
    float timer = 0;
    void FixedUpdate()
    {
        if(player != null) { 
            timer += Time.deltaTime;
            speedI.fillAmount = (player.GetComponent<DuckieMovement>().Speed - player.GetComponent<DuckieMovement>().BaseSpeed) / player.GetComponent<DuckieMovement>().BaseSpeed;
            scoreText.text = ScoreManager.instance.GetScore().ToString();
            timerText.text = Mathf.Round(timer).ToString();
            Vector3 goal = player.transform.position;
            if(Mathf.Round(timer) % 3 == 0 && !onceB)
            {
                onceB = true;
                Vector3 origin = new Vector3(Random.Range(-100, 100), -48, Random.Range(-50, 50));
                SpawnObject.Spawn(origin, goal, gos[0], player);
            }
            else if(Mathf.Round(timer) % 3 != 0 && onceB)
            {
                onceB = false;
            }
            if (Mathf.Round(timer) % 5 == 0 && !onceW)
            {
                onceW = true;
                float randX = Random.Range(0, 150);
                float randZ = Random.Range(0, 80);
                if (randX < 120)
                    randZ = Random.Range(60, 80);
                randX *= Random.Range(0, 2) * 2 - 1;
                randZ *= Random.Range(0, 2) * 2 - 1;
                Vector3 origin = new Vector3(randX, -50, randZ);
                SpawnObject.Spawn(origin, goal, gos[Random.Range(1, 3)], player);
            }
            else if (Mathf.Round(timer) % 5  != 0 && onceW)
            {
                onceW = false;
            }
            if (Mathf.Round(timer) % 3 == 0 && !onceM)
            {
                onceM = true;
                Vector3 origin = new Vector3(Random.Range(-100, 100), -50, Random.Range(-50, 50));
                SpawnObject.Spawn(origin, goal, gos[3], player);
            }
            else if(Mathf.Round(timer) % 3 != 0 && onceM) 
            {
                onceM = false;
            }
            if(Mathf.Round(timer) % 6 == 0 && !onceS) 
            {
                onceS = true;
                Vector3 origin = new Vector3(Random.Range(-10, 10) + player.transform.position.x, -50, Random.Range(-10, 10) + player.transform.position.z);
                SpawnObject.Spawn(origin, goal, gos[4], player);
            }
            else if(Mathf.Round(timer) % 6 != 0 && onceS)
            {
                onceS = false;
            }
            if (Mathf.Round(timer) % 30 == 0 && !onceP)
            {
                wp.pull = 3;
                onceP = true;
                Vector3 origin = new Vector3(Random.Range(-100, 100), -48, Random.Range(-50, 50));
                SpawnObject.Spawn(origin, goal, gos[5], player);
            }
            else if(Mathf.Round(timer) % 30 != 0 && onceP) 
            {
                wp.pull = .5f;
                onceP = false;
            }
        }
    }
}
