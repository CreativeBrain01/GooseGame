using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNetworking : MonoBehaviour
{
    public MonoBehaviour[] scriptsToIgnore;

    private PhotonView photonView;

    [SerializeField]
    TMP_Text characteNameBox;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
        }

        characteNameBox.text = PhotonNetwork.NickName;
    }
}
