using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Snowy.MultiplayerBasics
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public static Launcher Instance;
        public TMP_InputField playerNameInput;
        public TMP_Text status;

        #region Unity Methods

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            //Debug.Log("Connecting to Photon...");
            //PhotonNetwork.ConnectUsingSettings();
        }

        #endregion

        #region PUN Callbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Photon!");
            status.text = "Connecting to lobby...";
            PhotonNetwork.NickName = playerNameInput.text;
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
            base.OnConnectedToMaster();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected from Photon!");
            base.OnDisconnected(cause);
        }

        public override void OnJoinedLobby()
        {
            status.text = "Joined Lobby";
            Debug.Log("Joined Lobby!");
            MenuManager.Instance.OpenMenu("Start");
            base.OnJoinedLobby();
        }

        public override void OnJoinedRoom()
        {
            status.text = "Joined Room";
            Debug.Log("Joined Room!");
            if (PhotonNetwork.LocalPlayer.IsMasterClient) PhotonNetwork.LoadLevel("Map");
            //SceneManager.LoadScene("Map");
            base.OnJoinedRoom();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            status.text = "Create Room Failed";
            Debug.Log("Create Room Failed!");
            MenuManager.Instance.OpenMenu("Start");
            base.OnCreateRoomFailed(returnCode, message);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            status.text = "Join Random Failed";
            Debug.Log("Join Random Failed!");
            MenuManager.Instance.OpenMenu("Start");
            base.OnJoinRandomFailed(returnCode, message);
        }

        #endregion

        #region Public Methods

        public void ConnectToMaster()
        {
            // Check if the player name is empty && more than 2 characters
            if (string.IsNullOrEmpty(playerNameInput.text))
            {
                status.text = "Please enter a name!";
                return;
            }

            if (playerNameInput.text.Length < 2)
            {
                status.text = "Please enter a name with at least 2 characters!";
                return;
            }

            // Set the player name
            MenuManager.Instance.OpenMenu("Loading");
            PhotonNetwork.NickName = playerNameInput.text;
            status.text = "Connecting to Photon...";
            PhotonNetwork.ConnectUsingSettings();
        }

        public void JoinRoom()
        {
            status.text = "Joining Room...";
            MenuManager.Instance.OpenMenu("Loading");
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        #endregion
    }
}
