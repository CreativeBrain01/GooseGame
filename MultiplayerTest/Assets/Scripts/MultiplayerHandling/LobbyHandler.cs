using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyHandler : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject lobbyMenu;
    [Tooltip("The Dropdown menu for users to select what map they load into.")]
    [SerializeField]
    private Dropdown arenaSelection;
    [SerializeField]
    Text RoomCode;


    #region Private Methods

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            lobbyMenu.SetActive(false);
        }

        RoomCode.text = "Room Code: " + PhotonNetwork.CurrentRoom.Name;
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