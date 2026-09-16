using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public float spawnInterval = 0.015f;
    public float ghostLife = 0.5f;
    public float driftSpeed = 6f;
    [ColorUsage(false)]
    public Color ghostColor = new Color(1f, 1f, 1f, 0.6f);

    private SpriteRenderer playerSR;
    private float timer;

    void Start()
    {
        playerSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;
        timer = spawnInterval;

        GameObject ghost = new GameObject("TrailGhost");
        ghost.transform.SetPositionAndRotation(
            playerSR.transform.position,
            playerSR.transform.rotation);
        ghost.transform.localScale = playerSR.transform.lossyScale;

        SpriteRenderer sr = ghost.AddComponent<SpriteRenderer>();
        sr.sprite = playerSR.sprite;
        sr.flipX = playerSR.flipX;
        sr.flipY = playerSR.flipY;
        sr.sortingLayerID = playerSR.sortingLayerID;
        sr.sortingOrder = playerSR.sortingOrder - 1;
        sr.color = ghostColor;

        TrailGhostFade fade = ghost.AddComponent<TrailGhostFade>();
        fade.life = ghostLife;
        fade.driftSpeed = driftSpeed;
    }
}