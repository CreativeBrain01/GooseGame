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
        int index = 0;
        do
        {
            index = (int)Random.Range(0, spawnPoints.Length - 1);
        } while (usedSpawns.Contains(index));

        Transform chosenSpawn = spawnPoints[index].transform;

        PhotonNetwork.Instantiate("Prefabs/Player", chosenSpawn.position, Quaternion.identity);
    }
}
