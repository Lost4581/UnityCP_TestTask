using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float flyForce = 25f;
    public float maxSpeed = 10f;
    public LayerMask barrierLayer;

    private Rigidbody2D rb;
    private InputAction flyAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.freezeRotation = true;

        flyAction = new InputAction("Fly", InputActionType.Button);
        flyAction.AddBinding("<Keyboard>/space");
        flyAction.AddBinding("<Mouse>/leftButton");
        flyAction.Enable();
    }

    void OnDestroy()
    {
        flyAction.Disable();
        flyAction.Dispose();
    }

    void FixedUpdate()
    {
        if (flyAction.IsPressed())
            rb.AddForce(Vector2.up * flyForce, ForceMode2D.Force);

        float clampedY = Mathf.Clamp(rb.linearVelocity.y, -maxSpeed, maxSpeed);
        rb.linearVelocity = new Vector2(0, clampedY);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & barrierLayer) != 0)
            GameManager.Instance.Restart();
    }
}