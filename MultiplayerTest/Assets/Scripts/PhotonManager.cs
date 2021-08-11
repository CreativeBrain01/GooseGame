using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject[] spawnPoints;

    public List<int> usedSpawns = new List<int>();

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        int index = 0;
        do
        {
            index = (int)Random.Range(0, spawnPoints.Length - 1);
        } while (usedSpawns.Contains(index));

        Transform chosenSpawn = spawnPoints[index].transform;

        PhotonNetwork.Instantiate("Prefabs/Player", chosenSpawn.position, Quaternion.identity);
    }
}
