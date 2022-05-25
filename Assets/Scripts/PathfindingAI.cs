using UnityEngine;

public class PathfindingAI : MonoBehaviour
{
    public GameObject Player;
    private UnityEngine.AI.NavMeshAgent nav;

    public static bool PlayerInRange;
    int walkingRadius = 20;
    [SerializeField] int monsterSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = monsterSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PathFind();
    }

    void PathFind()
    {
        if (PlayerInRange)
        {
            nav.SetDestination(Player.transform.position);
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
