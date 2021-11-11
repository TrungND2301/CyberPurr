using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    int moveDirection = 1;
    float randomDropTime;
    float totalFlyTime;
    float hellicopterLifetime;

    [SerializeField] GameObject dogPrefab;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] GameObject fragments;
    ScoreKeeper scoreKeeper;
    bool isDropt = false;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        float timeToFlyAcrossScreen = (Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height) / moveSpeed;
        randomDropTime = Random.Range(0.0f, timeToFlyAcrossScreen);
        totalFlyTime = 0.0f;
        Destroy(gameObject, timeToFlyAcrossScreen + 1.0f);
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
        transform.localScale = new Vector2(Mathf.Sign(moveDirection), 1.0f);
    }

    public void SetMoveDirection(bool isMoveFromLeft)
    {
        moveDirection = isMoveFromLeft ? 1 : -1;
        FlipEnemyFacing();
    }

    void DropDog()
    {
        Instantiate(dogPrefab, transform.position, Quaternion.identity);
        isDropt = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            scoreKeeper.AddScore(1);
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
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void DropFragments()
    {
        GameObject instance = Instantiate(fragments, transform.position, Quaternion.identity);
        instance.GetComponentInChildren<HellicopterBrokenController>().SetDirection(moveDirection);
        Destroy(instance, 4.0f);
    }
}
