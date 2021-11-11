using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatBaseController : MonoBehaviour
{
    [SerializeField] float fireRate = 1.0f;

    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject fragments;
    [SerializeField] ParticleSystem explosionEffect;

    AudioPlayer audioPlayer;
    GameObject instance;
    Coroutine firingCoroutine;

    bool isFiring;
    bool isExploded;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        isFiring = false;
        isExploded = false;
        SpawnBullet();
    }

    void Update()
    {
        if (!isExploded)
            Fire();
    }

    void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
    }

    IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            instance.GetComponent<BulletController>().Fire();
            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(fireRate);
            SpawnBullet();
        }

        firingCoroutine = null;
    }

    void SpawnBullet()
    {
        instance = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Dog" &&
            !other.GetComponent<DogController>().isDogEliminated &&
            other.GetComponent<DogController>().isDogGroundTouched &&
            !isExploded)
        {
            isExploded = true;
            Destroy(instance);

            audioPlayer.PlayExplosionClip();
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
        Instantiate(fragments, transform.position, Quaternion.identity);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
