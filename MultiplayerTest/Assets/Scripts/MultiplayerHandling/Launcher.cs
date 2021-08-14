using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    /// <summary>
    /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
    /// </summary>
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [Tooltip("The Ui Panel to let the user enter name, connect and play.")]
    [SerializeField]
    private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress.")]
    [SerializeField]
    private GameObject progressLabel;

    [SerializeField]
    InputField roomCodeInput;
    
    #endregion


    #region Private Fields


    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    string gameVersion = "1";
    bool isConnecting;

    List<string> roomCodes;
    #endregion


    #region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
        roomCodes = new List<string>();
    }

    #endregion


    #region Public Methods


    /// <summary>
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    string theAlphabet = "abcdefghijklmnopqrstuvwxyz";

    string roomCode = "ABC123";
    public void HostLobby()
    {
        //Ensuring Username is OK
        //Generating Roomcode
        int nickL = PhotonNetwork.NickName.Length;
        int codeLength = Mathf.Clamp(nickL, 4, 8);

        do
        {
            string newCode = "";
            for (int i = 0; i < codeLength; i++)
            {
                switch (Mathf.RoundToInt(Random.Range(0, 2)))
                {
                    case 0:
                        newCode += theAlphabet[Mathf.RoundToInt(Random.Range(0, 25))];
                        break;

                    case 1:
                        newCode += Mathf.RoundToInt(Random.Range(0, 9));
                        break;

                    default:
                        Debug.LogError("Woops random code generation ran into an issue. This could cause long waiting times please fix.");
                        break;
                }
            }
            roomCode = newCode;
        } while (roomCode == "ABC123" || roomCodes.Contains(roomCode));

        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        isConnecting = PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    public void JoinLobby()
    {
        roomCode = roomCodeInput.text.Trim();

        if (roomCode != null && !roomCode.Contains(" "))
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

        if(!roomCodes.Contains(roomCode))
        {
            PhotonNetwork.CreateRoom(roomCode, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            roomCodes.Add(roomCode);
        }

        PhotonNetwork.JoinRoom(roomCode);
        isConnecting = false;
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        isConnecting = false;

        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);

        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

        PhotonNetwork.LoadLevel("Lobby");
    }

    #endregion
}
