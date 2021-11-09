using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    int moveDirection = 1;
    float randomDropTime;
    float totalFlyTime;

    [SerializeField] GameObject dogPrefab;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] GameObject fragments;
    bool isDropt = false;

    void Start()
    {
        float timeToFlyAcrossScreen = (Camera.main.orthographicSize * 2f) / moveSpeed;
        randomDropTime = Random.Range(0f, timeToFlyAcrossScreen);
        totalFlyTime = 0;
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += moveSpeed * moveDirection * Time.deltaTime;
        transform.position = position;
        totalFlyTime += Time.deltaTime;
        if (totalFlyTime > randomDropTime && !isDropt)
            DropDog();
    }

    void FlipEnemyFacing()
    {
        // GetComponent<SpriteRenderer>().flipX = moveDirection > 0 ? false : true;
        transform.localScale = new Vector2(Mathf.Sign(moveDirection), 1f);
    }

    public void SetMoveDirection(bool isMoveFromLeft)
    {
        moveDirection = isMoveFromLeft ? 1 : -1;
        FlipEnemyFacing();
    }

    void DropDog()
    {
        // Instantiate(dogPrefab, transform.position, Quaternion.identity);
        isDropt = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            PlayHitEffect();
            DropFragments();
            Destroy(gameObject);
        }
    }

    void PlayHitEffect()
    {
        if (explosionEffect != null)
        {
            ParticleSystem instance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            instance.Stop();

            var main = instance.main;
            main.duration = 0.5f;
            main.startLifetime = 1.0f;
            main.maxParticles = 1;

            var shape = instance.shape;
            shape.radius = 0.1f;

            var psr = instance.GetComponent<ParticleSystemRenderer>();
            psr.sortingLayerName = "Hellicopter";
            psr.sortingOrder = 1;

            instance.Play();
            Destroy(instance, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void DropFragments()
    {
        GameObject instance = Instantiate(fragments, transform.position, Quaternion.identity);
        instance.GetComponentInChildren<HellicopterBrokenController>().SetDirection(moveDirection);
    }
}
