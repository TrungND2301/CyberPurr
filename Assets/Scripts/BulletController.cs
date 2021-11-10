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
    public bool isFiringStatus { get { return isFiring; } }

    [SerializeField] ParticleSystem smokeEffect;
    ParticleSystem instance;
    void Start()
    {
        currentMouse = Mouse.current;
    }

    void Update()
    {
        if (isFiring)
        {
            // Move the bullet
            transform.position += transform.up * bulletSpeed * Time.deltaTime;
        }
        else
        {
            // Calculator direction and angle to mouse point
            mousePosition = Camera.main.ScreenToWorldPoint(currentMouse.position.ReadValue());
            direction = mousePosition - transform.position;
            angle = Vector2.SignedAngle(Vector2.up, direction);
            angle = Mathf.Clamp(angle, -90.0f, 90.0f);
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    public void Fire()
    {
        isFiring = true;
        PlaySmokeEffect();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hellicopter")
        {
            Destroy(gameObject);
        }
    }

    void PlaySmokeEffect()
    {
        if (smokeEffect != null)
        {
            instance = Instantiate(smokeEffect, transform.position, Quaternion.identity);
            instance.transform.parent = gameObject.transform;
            instance.transform.eulerAngles = gameObject.transform.eulerAngles;
            instance.Play();
        }
    }
}
