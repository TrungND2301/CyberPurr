using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    float moveDirection = 1f;
    float randomDropTime;
    float totalFlyTime;

    [SerializeField] GameObject dogPrefab;
    bool isDropt = false;

    void Start()
    {
        float timeToFlyAcrossScreen = (Camera.main.orthographicSize * 2f) / moveSpeed;
        randomDropTime = Random.Range(0f, timeToFlyAcrossScreen);
        totalFlyTime = 0;
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += moveSpeed * moveDirection * Time.deltaTime;
        transform.position = position;
        totalFlyTime += Time.deltaTime;
        if (totalFlyTime > randomDropTime && !isDropt)
            DropDog();
    }

    void FlipEnemyFacing()
    {
        // GetComponent<SpriteRenderer>().flipX = moveDirection > 0 ? false : true;
        transform.localScale = new Vector2(Mathf.Sign(moveDirection), 1f);
    }

    public void SetMoveDirection(bool isMoveFromLeft)
    {
        moveDirection = isMoveFromLeft ? 1f : -1f;
        FlipEnemyFacing();
    }

    void DropDog()
    {
        Instantiate(dogPrefab, transform.position, Quaternion.identity);
        isDropt = true;
    }
}
