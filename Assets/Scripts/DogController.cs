using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Animator animator;
    bool isFalling;
    bool isRunning;
    int direction;

    void Start()
    {
        animator = GetComponent<Animator>();

        direction = transform.position.x < 0 ? 1 : -1;
        if (direction < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        isFalling = true;
        isRunning = false;
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
        if (other.tag == "Bullet")
        {
            // Destroy(gameObject);
        }
        if (other.tag == "Background")
        {
            isFalling = false;
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
        if (other.tag == "Player")
        {
            isRunning = false;
            isFalling = false;
            animator.enabled = false;
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
