using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Client
public class GameController : MonoBehaviour
{
    [Header("Spawner settings")]
    public GameObject[] objects;
    private Spawner spawner;

    [Header("Lines settings")]
    public List<float> line1;
    public List<float> line2;
    public List<float> line3;
    public List<float> line4;
    public List<float> line5;
    public List<float> line6;
    public List<float> line7;
    public List<float> line8;
    public List<float> line9;
    public List<float> line10;

    [Header("Player settings")]
    public int startLives;
    public static int lives;

    [Header("GameController settings")]
    public Text livesText;
    public Image loseImage;
    public Image winImage;
    private AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip loseSound;

    public static int finishesReached;
    public static bool gameFinished;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        spawner = FindObjectOfType<Spawner>();

        // Spawn set objects on lines
        SpawnLine(line1, -3.5f, false, 2.5f,  0);   //car
        SpawnLine(line2, -2.5f, true,  1f,    0);   //car
        SpawnLine(line3, -1.5f, false, 2.5f,  0);   //car
        SpawnLine(line4, -0.5f, true,  7f,    0);   //car
        SpawnLine(line5,  0.5f, false, 1.75f, 1);   //truck
        SpawnLine(line6,  2.5f, false, 4f,    6);   //turtle
        SpawnLine(line7,  3.5f, true,  1f,    2);   //log
        SpawnLine(line8,  4.5f, true,  2.5f,  4);   //log
        SpawnLine(line9,  5.5f, false, 2.5f,  5);   //turtle
        SpawnLine(line10, 6.5f, true,  2.5f,  3);   //log

        lives = startLives;
        livesText.text = lives.ToString();

        finishesReached = 0;
        gameFinished = false;
    }

    void SpawnLine(List<float> line, float y, bool directionRight, float speed, int objectID)
    {
        bool alreadyDiving = false;     // turtle variable

        foreach (float x in line)
        {
            GameObject prop = new GameObject();     // needed for turtle

            spawner.Spawn(objects[objectID], new Vector3(x, y, 0f), directionRight, speed, out prop);

            // if object is a turtle, then let the first one dive
            if ((objectID.Equals(6) || objectID.Equals(5)) && !alreadyDiving)
            {
                prop.GetComponent<Turtle>().isDiving = true;
                alreadyDiving = true;
            }
        }
    }

    private void Update()
    {
        if (gameFinished)
        {
            return;
        }

        livesText.text = lives.ToString();

        if (finishesReached == 5)
        {
            Win();
        }
        else if (lives <= 0)
        {
            Lose();
        }
    }


    private void Lose()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);

        gameFinished = true;

        // Show YOU DIED
        loseImage.enabled = true;

        audioSource.clip = loseSound;
        audioSource.Play();
    }

    private void Win()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);

        gameFinished = true;

        // Show YOU WIN
        winImage.enabled = true;

        audioSource.clip = winSound;
        audioSource.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}