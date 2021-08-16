using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviour
{
    private PhotonView pView;

    [SerializeField]
    float fireRate;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform shootPosition;

    float fireTimer;

    private void Start()
    {
        pView = GetComponentInParent<PhotonView>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && fireTimer >= fireRate)
        {
            fireTimer = 0;
            pView.RPC("RPCShoot", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RPCShoot()
    {
        GameObject newBullet = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        newBullet.GetComponent<BulletMovement>().Shoot(gameObject);
    }
}
