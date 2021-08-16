using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;


namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// Player name input field. Let the user input his name, will appear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        [SerializeField]
        GameObject hostplayDropdown;


        #region Private Constants

        // Store the PlayerPref Key to avoid typos
        const string playerNamePrefKey = "PlayerName";

        #endregion


        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;

            hostplayDropdown.SetActive(CheckNameLength(PhotonNetwork.NickName));
        }

        #endregion


        #region Public Methods

        bool CheckNameLength(string name)
        {
            return (name.Trim().Length >= 3 && name.Trim().Length < 20);
        }

        /// <summary>
        /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
        /// </summary>
        /// <param name="value">The name of the Player</param>
        public void SetPlayerName(string value)
        {
            bool validName = CheckNameLength(value);

            hostplayDropdown.SetActive(validName);
            if (validName)
            {
                // #Important
                if (string.IsNullOrEmpty(value.Trim()))
                {
                    Debug.LogError("Player Name is null or empty");
                    return;
                }
                PhotonNetwork.NickName = value.Trim();

                PlayerPrefs.SetString(playerNamePrefKey, value.Trim());
            }
        }

        #endregion
    }
}