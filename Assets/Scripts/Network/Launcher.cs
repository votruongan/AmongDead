﻿
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject controlPanel;

        [SerializeField]
        private Text feedbackText;

        [SerializeField]
        private byte maxPlayersPerRoom = 10;

        bool isConnecting;

        string gameVersion = "1";

        [Space(10)]
        [Header("Custom Variables")]
        public InputField playerNameField;
        public InputField roomNameField;

        [Space(5)]
        public Text playerStatus;
        public Text connectionStatus;

        [Space(5)]
        public GameObject roomJoinUI;
        public GameObject buttonLoadArena;
        public GameObject buttonJoinRoom;

        string playerName = "";
        string roomName = "";

        // Start Method
        void Start(){
            PlayerPrefs.DeleteAll();
            Debug.Log("Connecting to Photon Network");

            roomJoinUI.SetActive(false);
            buttonLoadArena.SetActive(false);
            ConnectToPhoton();
        }

        void Awake(){
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        // Helper Methods
        public void SetPlayerName(string name){
            playerName = name;
        }

        public void SetRoomName (string name){
            roomName = name;
        }

        // Tutorial Methods
        void ConnectToPhoton(){
            connectionStatus.text = "Connecting...";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void JoinRoom(){
            if (PhotonNetwork.IsConnected){
                PhotonNetwork.LocalPlayer.NickName = playerName;
                Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " + 
                roomNameField.text);
                RoomOptions roomOptions = new RoomOptions();
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default);
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby);
            }
        }

        public void LoadArea() {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1) {
                PhotonNetwork.LoadLevel("MainArena");
            }
            else {
                playerStatus.text = "Minimum 2 Players required to Load Arena!";
            }
        }
        // Photon Methods
        public override void OnConnected(){
            base.OnConnected();
            connectionStatus.text = "Connected to Photon!";
            connectionStatus.color = Color.green;
            roomJoinUI.SetActive(true);
            buttonLoadArena.SetActive(false);
        }
        public override void OnDisconnected(DisconnectCause cause) {
            isConnecting = false;
            controlPanel.SetActive(true);
            Debug.LogError("Disconnected. Please check your Internet connection");
        }

        public override void OnJoinedRoom(){
            if (PhotonNetwork.IsMasterClient){
                buttonLoadArena.SetActive(true);
                buttonJoinRoom.SetActive(false);
                playerStatus.text = "You are Lobby Leader";
            }
            else {
                playerStatus.text = "Connected to Lobby";
            }
        }
    }
}
