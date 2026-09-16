using UnityEngine;

public class TrailGhostFade : MonoBehaviour
{
    public float life = 0.5f;
    public float driftSpeed = 6f;

    private SpriteRenderer sr;
    private float age;
    private Color startColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
    }

    void Update()
    {
        age += Time.deltaTime;
        float t = age / life;

        if (t >= 1f)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += Vector3.left * (driftSpeed * Time.deltaTime);

        Color c = startColor;
        c.a = startColor.a * (1f - t);
        sr.color = c;
    }
}