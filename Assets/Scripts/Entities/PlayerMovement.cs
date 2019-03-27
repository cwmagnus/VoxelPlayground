using UnityEngine;
using WorldGeneration;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float loadDistance;
    [SerializeField] private float reloadChunkDistance;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpBuffer;
    private World world;
    private Vector3 lastPosition;

    private Rigidbody body;

    private ModifyTerrain modify;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        modify = GetComponentInChildren<ModifyTerrain>();
    }

    private void Start()
    {
        world = FindObjectOfType<World>();
        StartCoroutine(world.LoadChunks(transform.position, loadDistance));
    }

    private void Update()
    {
        float distanceMoved = Vector2.Distance(new Vector2(lastPosition.x, lastPosition.z),
            new Vector2(transform.position.x, transform.position.z));
        if (distanceMoved > reloadChunkDistance)
        {
            lastPosition = transform.position;
            StartCoroutine(world.LoadChunks(transform.position, loadDistance));
        }

        if (Input.GetMouseButtonDown(0))
        {
            modify.ReplaceBlockCenter(10, -.0000000001f);
        }

        if (Input.GetMouseButtonDown(1))
        {
            modify.AddBlockCenter(10, .00000000001f);
        }
    }

    private void FixedUpdate()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        Vector3 velocity = (translation * transform.forward) + (straffe * transform.right);
        velocity.y = body.velocity.y;
        body.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space) && (body.velocity.y > -jumpBuffer && body.velocity.y < jumpBuffer))
        {
            body.AddForce(Vector3.up * jumpForce);
        }
    }
}
