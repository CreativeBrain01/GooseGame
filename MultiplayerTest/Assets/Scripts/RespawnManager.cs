using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviourPunCallbacks
{
    private static RespawnManager instance;
    public static RespawnManager Instance { get => instance; }

    public PhotonManager playerManager;

    private PhotonView pv;

    private bool isRespawning = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectsOfType<PlayerMovement>().Length <= 1)
        {
            if (PhotonNetwork.IsMasterClient && !isRespawning)
            {
                PhotonNetwork.DestroyAll();
                photonView.RPC("Respawn", RpcTarget.All);
                isRespawning = true;
            }
        }
        else
        {
            if (isRespawning)
            {
                isRespawning = false;
            }
        }
    }

    [PunRPC]
    void Respawn()
    {
        playerManager.Spawn();
    }
}
