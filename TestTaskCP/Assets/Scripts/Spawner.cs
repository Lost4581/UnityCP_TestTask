using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject redCubePrefab;
    public GameObject yellowCubePrefab;

    [Header("Игровое поле")]
    public float leftX = -10f;
    public float rightX = 10f;
    public float bottomY = -5f;
    public float topY = 5f;

    [Header("Спавн")]
    public float spawnInterval = 1.2f;
    public float redChance = 0.4f;
    public float spawnJitter = 1f;

    [Header("Полёт")]
    public float minFlightTime = 0.8f;
    public float maxFlightTime = 1.6f;
    public float spread = 0.5f;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Spawn();
            timer = spawnInterval;
        }
    }

    void Spawn()
    {
        bool isRed = Random.value < redChance;
        GameObject prefab = isRed ? redCubePrefab : yellowCubePrefab;

        Vector2 spawnPos = new Vector2(
            rightX + Random.Range(0f, spawnJitter),
            bottomY - Random.Range(0f, spawnJitter));

        Vector2 target = new Vector2(
            leftX,
            Random.Range(bottomY, topY));

        target += Random.insideUnitCircle * spread;

        GameObject cube = Instantiate(prefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = cube.GetComponent<Rigidbody2D>();

        float g = Physics2D.gravity.y * rb.gravityScale;
        float t = Random.Range(minFlightTime, maxFlightTime);

        Vector2 v;
        v.x = (target.x - spawnPos.x) / t;
        v.y = (target.y - spawnPos.y) / t - 0.5f * g * t;

        rb.linearVelocity = v;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(leftX, bottomY), new Vector3(rightX, bottomY));
        Gizmos.DrawLine(new Vector3(leftX, topY), new Vector3(rightX, topY));
        Gizmos.DrawLine(new Vector3(leftX, bottomY), new Vector3(leftX, topY));
        Gizmos.DrawLine(new Vector3(rightX, bottomY), new Vector3(rightX, topY));

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(rightX, bottomY), 0.3f);

        Gizmos.color = Color.yellow;
        foreach (float targetY in new[] { bottomY, (bottomY + topY) * 0.5f, topY })
        {
            Vector2 dir = (new Vector2(leftX, targetY) - new Vector2(rightX, bottomY)).normalized;
            Gizmos.DrawRay(new Vector3(rightX, bottomY), dir * 5f);
        }
    }
}