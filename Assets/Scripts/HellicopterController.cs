using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    float moveDirection = -1f;

    void Start()
    {
        if (moveDirection < 0f)
        {
            FlipEnemyFacing();
        }
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += moveSpeed * moveDirection * Time.deltaTime;
        transform.position = position;
    }

    void FlipEnemyFacing()
    {
        // GetComponent<SpriteRenderer>().flipX = true;
        transform.localScale = new Vector2(Mathf.Sign(moveDirection), 1f);
    }
}
