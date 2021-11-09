using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    Mouse currentMouse;
    Vector3 mousePosition;
    Vector2 direction;
    float angle;

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
        direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(Vector2.up, direction);
        angle = Mathf.Clamp(angle, -90.0f, 90.0f);
        transform.eulerAngles = new Vector3(0, 0, angle);
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
