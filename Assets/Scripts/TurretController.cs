using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    [SerializeField] float fireRate = 1f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gun;
    GameObject instance;
    Coroutine firingCoroutine;
    bool isFiring;

    void Start()
    {

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
}
