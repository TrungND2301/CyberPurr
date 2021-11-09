using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatBaseController : MonoBehaviour
{
    [SerializeField] float fireRate = 1.0f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] Transform gun;
    [SerializeField] GameObject fragments;
    GameObject instance;
    Coroutine firingCoroutine;
    bool isFiring;
    bool isExploded;

    void Start()
    {
        isExploded = false;
    }

    void Update()
    {
        Fire();
    }

    void OnFire(InputValue value)
    {
        isFiring = value.isPressed;

        if (instance != null)
        {
            if (isFiring && firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(Fire());
            }
        }
        else
        {
            SpawnBullet();
        }
    }

    IEnumerator Fire()
    {
        instance.GetComponent<BulletController>().Fire();

        yield return new WaitForSeconds(fireRate);

        SpawnBullet();
        isFiring = false;
        firingCoroutine = null;
    }

    void SpawnBullet()
    {
        instance = Instantiate(bulletPrefab,
                                gun.position,
                                Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dog" && !isExploded)
        {
            isExploded = true;
            PlayExplosionEffect();
            ExplodeFragments();
        }
    }

    void PlayExplosionEffect()
    {
        if (explosionEffect != null)
        {
            ParticleSystem ps = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            ps.Stop();

            var main = ps.main;
            main.duration = 3.0f;
            main.startLifetime = 1.0f;
            main.maxParticles = 10;

            var shape = ps.shape;
            shape.radius = 2.0f;

            var psr = ps.GetComponent<ParticleSystemRenderer>();
            psr.sortingLayerName = "Player";
            psr.sortingOrder = 1;

            ps.Play();
            Destroy(instance, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }

    void ExplodeFragments()
    {
        GameObject instance = Instantiate(fragments, transform.position, Quaternion.identity);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
