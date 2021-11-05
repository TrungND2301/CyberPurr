using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;

    bool isFiring = false;

    void Start()
    {

    }

    void Update()
    {
        if (isFiring)
        {
            Vector2 position = transform.position;
            position.y += bulletSpeed * Time.deltaTime;
            transform.position = position;
        }
    }

    public void Fire()
    {
        isFiring = true;
    }
}
