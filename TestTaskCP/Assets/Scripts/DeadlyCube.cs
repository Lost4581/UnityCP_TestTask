using UnityEngine;

public class DeadlyCube : MonoBehaviour
{
    public LayerMask playerLayer;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & playerLayer) != 0)
            GameManager.Instance.Restart();
    }

    void Update()
    {
        if (transform.position.y < -10f || transform.position.x < -15f)
            Destroy(gameObject);
    }
}