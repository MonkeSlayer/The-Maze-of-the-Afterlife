using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // This is for Respawn();

    public Vector3 playerStartPosition;
    public static bool dead;

    Rigidbody rigidbody;
    Transform deathText;

    [SerializeField] AudioClip deathSound;
    AudioSource audioSource;

    void Start()
    {
        playerStartPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();

        deathText = transform.Find("Death Text");
        deathText.GetComponent<MeshRenderer>().enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "End":
                SceneManager.LoadScene(2);
                break;

            case "Play Again":
                SceneManager.LoadScene(1);
                break;

            case "Monster":
                StartRespawnProcess();
                break;
        }
    }

    void StartRespawnProcess()
    {
        audioSource.clip = deathSound;
        audioSource.Play();

        deathText.GetComponent<MeshRenderer>().enabled = true;

        Invoke("Respawn", 5);
        dead = false;
    }

    void Respawn()
    {
        transform.position = playerStartPosition;
        deathText.GetComponent<MeshRenderer>().enabled = false;
    }
}
