using UnityEngine;

public class PathfindingAI : MonoBehaviour
{
    public GameObject Player;
    AudioSource audioSource;
    private UnityEngine.AI.NavMeshAgent nav;

    [SerializeField] int monsterSpeed = 5;

    private static bool PlayerInRange;
    int walkingRadius = 20;

    // For respawn reset
    public static Vector3 monsterStartingPostion;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = monsterSpeed;
        audioSource = GetComponent<AudioSource>();
        monsterStartingPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PathFind();
        // For respawn reset
        CheckPlayerStatus();
    }

    void CheckPlayerStatus()
    {
        if (CollisionHandler.dead)
        {
            Reset();
        }
    }

    void Reset()
    {
        transform.position = monsterStartingPostion;
    }

    void PathFind()
    {
        if (PlayerInRange)
        {
            nav.SetDestination(Player.transform.position);
            audioSource.Play();
        }

        else
        {
            nav.SetDestination(RandomNavmeshPoint());

        }
    }

    private Vector3 RandomNavmeshPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkingRadius;
        randomDirection += transform.position;

        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, walkingRadius, 1);
        Vector3 finalPosition = hit.position;

        return finalPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInRange = false;
        }
    }
}
