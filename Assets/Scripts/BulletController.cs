using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1.0f;
    [SerializeField] float projectileLifetime = 2.0f;

    Vector3 mousePosition;
    Vector2 direction;
    float angle;

    bool isFiring = false;
    public bool isFiringStatus { get { return isFiring; } }

    [SerializeField] ParticleSystem smokeEffect;
    ParticleSystem instance;

    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, CalculateAngleToMousePoint());
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
            transform.eulerAngles = new Vector3(0.0f, 0.0f, CalculateAngleToMousePoint());
        }
    }

    float CalculateAngleToMousePoint()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(Vector2.up, direction);
        angle = Mathf.Clamp(angle, -90.0f, 90.0f);

        return angle;
    }

    public void Fire()
    {
        isFiring = true;
        PlaySmokeEffect();
        Destroy(gameObject, projectileLifetime);
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
