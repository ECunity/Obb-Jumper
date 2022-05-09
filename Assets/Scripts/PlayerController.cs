using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    private bool isMoving;
    private bool isMovingRight;
    private Rigidbody2D rb;
    // Limits x velocity
    private float maxVelX = 10;

    private Animator animator;
    private SpriteRenderer sr;

    public float xSpeed;
    public float jumpStrength;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        isMoving = false;
        isMovingRight= true;
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
            animator.SetBool("isGrounded", false);
            rb.AddForce(transform.up * vy);
        }

        rb.velocity = new Vector2(Vector2.ClampMagnitude(rb.velocity, maxVelX).x, rb.velocity.y);

        if (xHat >= 1) {
            isMovingRight = true;
            sr.flipX = false;
        }

        if (xHat <= -1) {
            isMovingRight = false;
            sr.flipX = true;
        }

        animator.SetBool("isMoving", isMoving);
        isMoving = xHat != 0 && yHat == 0;
        Debug.Log(isMoving);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        isGrounded = collision.gameObject.tag == "Ground";
        animator.SetBool("isGrounded", isGrounded);

        if (collision.gameObject.tag == "Respawn")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}