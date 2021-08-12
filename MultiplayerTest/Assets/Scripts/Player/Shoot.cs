using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    float fireRate;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform shootPosition;

    float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && fireTimer >= fireRate)
        {
            fireTimer = 0;
            GameObject newBullet = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
            newBullet.GetComponent<BulletMovement>().Shoot(gameObject);
        }
    }
}
