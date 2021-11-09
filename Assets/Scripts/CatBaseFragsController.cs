using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBaseFragsController : MonoBehaviour
{
    Rigidbody2D rdbody;
    [SerializeField] float jumpSpeed = 5.0f;

    void Awake()
    {
        rdbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rdbody.velocity += new Vector2(0.0f, jumpSpeed);
    }
}
