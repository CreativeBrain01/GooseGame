using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject[] spawnPoints;

    public List<int> usedSpawns = new List<int>();

    private void Start()
    {
        RespawnManager.Instance.playerManager = this;
        Spawn();
    }

    public void Spawn()
    {
        int index = 0;
        do
        {
            index = (int)Random.Range(0, spawnPoints.Length - 1);
        } while (usedSpawns.Contains(index));

        usedSpawns.Clear();

        Transform chosenSpawn = spawnPoints[index].transform;

        PhotonNetwork.Instantiate("Prefabs/Player", chosenSpawn.position, Quaternion.identity);

        Debug.Log(PhotonNetwork.ActivePlayers);
    }
}
