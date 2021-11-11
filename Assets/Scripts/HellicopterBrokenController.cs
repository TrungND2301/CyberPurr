using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterBrokenController : MonoBehaviour
{
    float rotateSpeed = 1.0f;
    float moveSpeed = 1.0f;
    int direction = 1;

    void Start()
    {
        rotateSpeed = Random.Range(50.0f, 100.0f);
        moveSpeed = Random.Range(-5.0f, 5.0f);
    }

    void Update()
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime * direction, Space.Self);
    }

    public void SetDirection(int direction)
    {
        this.direction = -direction;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Background")
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(gameObject, 2.0f);
        }
    }
}
