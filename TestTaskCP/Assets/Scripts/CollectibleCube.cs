using UnityEngine;

public class CollectibleCube : MonoBehaviour
{
    public LayerMask playerLayer;
    public float magnetRadius = 3f;
    public float magnetSpeed = 15f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isCollected;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (isCollected || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < magnetRadius)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            transform.position = Vector2.MoveTowards(
                transform.position, player.position,
                magnetSpeed * Time.deltaTime);
        }

        if (transform.position.y < -10f || transform.position.x < -15f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isCollected) return;

        if (((1 << col.gameObject.layer) & playerLayer) != 0)
        {
            isCollected = true;
            GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}