using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    void Start()
    {

    }

    void Update()
    {
        Vector2 position = transform.position;
        position.y += -moveSpeed * Time.deltaTime;
        transform.position = position;
    }
}
