using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb2D;
    Animator animator;
    ScoreKeeper scoreKeeper;
    bool isFalling;
    bool isRunning;
    int direction;
    bool isEliminated;
    bool isGroundTouched;
    public bool isDogEliminated { get { return isEliminated; } }
    public bool isDogGroundTouched { get { return isGroundTouched; } }
    float thrust = 5.0f;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        direction = transform.position.x < 0 ? 1 : -1;
        if (direction < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        isFalling = true;
        isRunning = false;
        isEliminated = false;
        isGroundTouched = false;
    }

    void Update()
    {
        if (isFalling)
            Falling();
        else if (isRunning)
            Running();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" && other.GetComponent<BulletController>().isFiringStatus)
        {
            isEliminated = true;
            isRunning = false;
            isFalling = false;
            animator.enabled = false;

            scoreKeeper.AddScore(1);

            transform.eulerAngles = other.GetComponent<Transform>().eulerAngles;
            rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            rb2D.AddTorque(5.0f);
            GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            GetComponent<BoxCollider2D>().isTrigger = false;

            Destroy(gameObject, 4.0f);
        }
        if (other.tag == "Background" && !isEliminated)
        {
            isFalling = false;
            isRunning = true;
            isGroundTouched = true;
            animator.SetBool("isRunning", true);
        }
        if (other.tag == "Player" && isGroundTouched)
        {
            isRunning = false;
            isFalling = false;
            animator.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Background" && isEliminated)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(gameObject, 2.0f);
        }
    }

    void Falling()
    {
        Vector2 position = transform.position;
        position.y += -moveSpeed * Time.deltaTime;
        transform.position = position;
    }

    void Running()
    {
        Vector2 position = transform.position;
        position.x += -moveSpeed * Time.deltaTime * -direction;
        transform.position = position;
    }
}
