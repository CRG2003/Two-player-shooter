using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class worldController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyR;
    public GameObject boss;
    public GameObject spawner;

    private AudioSource audio;
    public AudioClip surviveM;
    public AudioClip bossM;

    GameObject choice;
    GameObject player;
    GameObject player2;

    public Text timerText;
    public Text healthText;
    public Text freezeText;
    public Text powerUpTime;
    public float gameTimer;
    public float freezeTimer = 3.0f;
    public float bossHealth;
    public float animTimer;
    float spawnerTimer;
    float endTimer;

    public bool timeStop = false;
    public bool animFreeze = false;
    bool spawned = false;
    bool ran = false;
    public bool spawnerSpawned = false;

    public float spawnInterval;
    float spawnTimer;
    float stopTimer;
    float musicTimer = 31f;
    public float powerUpTimer = 0.01f;
    float switchTimer;
    int wall;

    public string stage = "Survive";

    void Start()
    {
        player = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(surviveM);
        audio.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && switchTimer <= 0){
            if (timeStop == true){
                timeStop = false;
            }
            else if (switchTimer <= 0){
                timeStop = true;
            }
            stopTimer = 3;
            switchTimer = 0.5f;
        }
        if (player != null){
            if (timeStop == false){
                freezeTimer = 3.01f;
                gameTimer -= Time.deltaTime;
                powerUpTimer -= Time.deltaTime;
                spawnerTimer -= Time.deltaTime;
                timerText.text = gameTimer.ToString();
            }
            if (gameTimer <= 0){
                stage = "Boss";
                timerText.text = "--";
            }
            else{
                freezeTimer -= Time.deltaTime;
            }
            stopTimer -= Time.deltaTime;
            switchTimer -= Time.deltaTime;

            if (powerUpTimer <= 0){
                powerUpTimer = 0.01f;
            }

            powerUpTime.text = powerUpTimer.ToString();
            healthText.text = player.GetComponent<PlayerController>().health.ToString();
            freezeText.text = freezeTimer.ToString();
            musicTimer -= Time.deltaTime;

            if (stopTimer <= 0){
                timeStop = false;
            }

            if (stage == "Survive"){
                if (musicTimer <= 0){
                    audio.PlayOneShot(surviveM);
                    musicTimer = 31f;
                }
                if (Random.Range(-1f, 1f) > 0){
                    choice = enemy;
                }
                else{
                    choice = enemyR;
                }
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0){
                    wall = Random.Range(1, 4);
                    if (wall == 1){
                        Instantiate(choice, new Vector3(-49, 1, Random.Range(-23f, 23f)), Quaternion.identity);
                    }
                    else if (wall == 2){
                        Instantiate(choice, new Vector3(49, 1, Random.Range(-23f, 23f)), Quaternion.identity);
                    }
                    else if (wall == 3){
                        Instantiate(choice, new Vector3(Random.Range(-48f, 48f), 1, 24), Quaternion.identity);
                    }
                    else if (wall == 4){
                        Instantiate(choice, new Vector3(Random.Range(-48f, 48f), 1, -24), Quaternion.identity);
                    }
                    spawnTimer = spawnInterval;
                }
                if (spawnerSpawned == false && gameTimer <= 40f && spawnerTimer <= 0){
                    wall = Random.Range(1, 4);
                    if (wall == 1){
                        Instantiate(spawner, new Vector3(-40, 2.5f, 17), Quaternion.identity);
                    }
                    else if (wall == 2){
                        Instantiate(spawner, new Vector3(40, 2.5f, 17), Quaternion.identity);
                    }
                    else if (wall == 3){
                        Instantiate(spawner, new Vector3(40, 2.5f, -17), Quaternion.identity);
                    }
                    else if (wall == 4){
                        Instantiate(spawner, new Vector3(-40, 2.5f, -17), Quaternion.identity);
                    }
                    spawnerSpawned = true;
                    spawnerTimer = 12;
                }
            }
            else if (stage == "Boss"){
                animFreeze = true;
                animTimer -= Time.deltaTime;
                if (animTimer <= 0){
                    animFreeze = false;
                }
                if (spawned == false){
                    player.transform.position = new Vector3(25, 1.5f, 0);
                    player2.transform.position = new Vector3(-25, 1.5f, 0);
                    boss = Instantiate(boss, transform.position, transform.rotation);
                    audio.Stop();
                    audio.PlayOneShot(bossM);
                    audio.volume = 0.2f;
                    spawned = true;
                }
                if (boss != null){
                    bossHealth = boss.GetComponent<bossController>().health;
                }
                else{
                    stage = "victory";
                    if (ran == false){
                        endTimer = 3;
                        ran = true;
                    }
                    endTimer -= Time.deltaTime;
                    if (endTimer <= 0){
                        SceneManager.LoadScene("menu");
                    }
                }
            }
        }
        else{
            stage = "failure";
            if (ran == false){
                endTimer = 3;
                ran = true;
            }
            endTimer -= Time.deltaTime;
            if (endTimer <= 0){
                SceneManager.LoadScene("menu");
            }
        }
    }
}
