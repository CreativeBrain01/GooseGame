using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyHandler : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject lobbyMenu;
    [Tooltip("The Dropdown menu for users to select what map they load into.")]
    [SerializeField]
    private Dropdown arenaSelection;
    [SerializeField]
    Text RoomCode;

    [SerializeField]
    Text textCopied;

    [SerializeField]
    Text[] playerNameTxt;

    #region Private Methods

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            lobbyMenu.SetActive(false);
        }

        RoomCode.text = "Room Code: " + PhotonNetwork.CurrentRoom.Name;
    }

    private void Update()
    {
        if (txtCopyTimer > 0.0f)
        {
            txtCopyTimer -= Time.deltaTime;
        } else
        {
            textCopied.text = "";
        }

        List<Player> players = new List<Player>();
        players.AddRange(PhotonNetwork.PlayerList);

        switch (players.Count)
        {
            case 1:
                playerNameTxt[0].text = players[0].NickName;
                playerNameTxt[1].transform.parent.gameObject.SetActive(false);
                playerNameTxt[2].transform.parent.gameObject.SetActive(false);
                playerNameTxt[3].transform.parent.gameObject.SetActive(false);
                break;

            case 2:
                playerNameTxt[0].text = players[0].NickName;
                playerNameTxt[1].text = players[1].NickName;
                playerNameTxt[1].transform.parent.gameObject.SetActive(true);
                playerNameTxt[2].transform.parent.gameObject.SetActive(false);
                playerNameTxt[3].transform.parent.gameObject.SetActive(false);
                break;

            case 3:
                playerNameTxt[0].text = players[0].NickName;
                playerNameTxt[1].text = players[1].NickName;
                playerNameTxt[2].text = players[2].NickName;
                playerNameTxt[1].transform.parent.gameObject.SetActive(true);
                playerNameTxt[2].transform.parent.gameObject.SetActive(true);
                playerNameTxt[3].transform.parent.gameObject.SetActive(false);
                break;

            case 4:
                playerNameTxt[0].text = players[0].NickName;
                playerNameTxt[1].text = players[1].NickName;
                playerNameTxt[2].text = players[2].NickName;
                playerNameTxt[3].text = players[3].NickName;
                playerNameTxt[1].transform.parent.gameObject.SetActive(true);
                playerNameTxt[2].transform.parent.gameObject.SetActive(true);
                playerNameTxt[3].transform.parent.gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }

    float txtCopyTimer = 0.0f;
    public void CopyCode()
    {
        textCopied.text = "Code copied to clipboard!!";
        txtCopyTimer = 5.0f;
        Utility.CopyToClipboard(PhotonNetwork.CurrentRoom.Name);
    }

    public void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }

        switch (arenaSelection.value)
        {
            case 0:
                PhotonNetwork.LoadLevel("Arena1");
                break;

            case 1:
                PhotonNetwork.LoadLevel("Arena2");
                break;

            case 2:
                PhotonNetwork.LoadLevel("Arena3");
                break;

            default:
                Debug.LogError("Failed to load scene. Parhaps it doesn't exist?");
                break;
        }
    }

    #endregion

    #region Photon Callbacks


    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    #endregion


    #region Public Methods


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    #endregion
}