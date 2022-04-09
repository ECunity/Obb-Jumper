using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    private bool isMoving;
    private Rigidbody2D rb;
    // Limits x velocity
    private float maxVelX = 10;

    public float xSpeed;
    public float jumpStrength;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        isMoving = false;
    }

    // Update is called once per frame. FixedUpdate is used for physics (Rigidbody2D)
    void FixedUpdate()
    {
        float xHat = new Vector2(Input.GetAxis("Horizontal"), 0).normalized.x;
        float vx = xHat * xSpeed;
        rb.AddForce(transform.right * vx);

        float yHat = new Vector2(0, Input.GetAxis("Vertical")).normalized.y;
        if (isGrounded && yHat == 1) {
            float vy = yHat * jumpStrength;
            isGrounded = false;
            rb.AddForce(transform.up * vy);
        }

        rb.velocity = new Vector2(Vector2.ClampMagnitude(rb.velocity, maxVelX).x, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        isGrounded = collision.gameObject.tag == "Ground";
    }
}
