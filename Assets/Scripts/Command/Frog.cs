using UnityEngine;

public class Frog : MonoBehaviour
{
    private Vector3 spawnPoint;

    [Header("Object settings")]
    public AudioClip jumpSound;
    public AudioClip deathSound;
    private AudioSource audioSource;


    [HideInInspector]
    public bool isGrounded;

    [HideInInspector]
    public string killedBy;         // for testing purpose

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        killedBy = null;

        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;

        isGrounded = true;
    }

    void Update()
    {
        if (!isGrounded)
        {
            killedBy = "Unwalkable tiles";
            Death();
        }

        if (transform.position.x >= 8f || transform.position.x <= -8f)      // if the frog is outside of game boundary
        {
            killedBy = "Boundary";
            Death();
        }
    }

    private void Death()
    {
        GameController.lives--;

        PlayDeathSound();

        Respawn();
    }

    private void Respawn()
    {
        transform.position = spawnPoint;
        isGrounded = true;
    }

    private void Finished()
    {
        GameObject frogDummy = Instantiate(gameObject);
        frogDummy.transform.parent = null;
        Destroy(frogDummy.GetComponent<Frog>());

        GameController.finishesReached++;

        Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish") && !collision.CompareTag("Frog"))
        {
            collision.enabled = false;
            isGrounded = true;

            Finished();
        }
        else if (collision.CompareTag("Walkable"))
        {
            isGrounded = true;
        }
        else if (collision.CompareTag("Log") || collision.CompareTag("Turtle"))
        {
            gameObject.transform.SetParent(collision.transform);
            isGrounded = true;
        }
        else
        {
            if (collision.gameObject.name.Contains("Car"))
            {
                killedBy = "Car";
            }
            else if (collision.gameObject.name.Contains("Truck"))
            {
                killedBy = "Truck";
            }

            Death();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Log") || collision.CompareTag("Turtle"))
        {
            gameObject.transform.parent = null;
            isGrounded = false;
        }
        else if (collision.CompareTag("Walkable"))
        {
            isGrounded = false;
        }
    }

    public void PlayJumpSound()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
    }


    public void PlayDeathSound()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
    }
}