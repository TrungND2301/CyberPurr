using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    Mouse currentMouse;
    Vector2 mousePosition;

    bool isFiring = false;

    void Start()
    {
        currentMouse = Mouse.current;
    }

    void Update()
    {
        // if (isFiring)
        // {
        //     Vector2 position = transform.position;
        //     position.y += bulletSpeed * Time.deltaTime;
        //     transform.position = position;
        // }

        mousePosition = Camera.main.ScreenToWorldPoint(currentMouse.position.ReadValue());
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, 1f * Time.deltaTime);
        Debug.Log(mousePosition.x + " " + mousePosition.y);
    }

    public void Fire()
    {
        isFiring = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hellicopter")
            Destroy(gameObject);
    }
}
