using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject PlayerNameMenu;
    [SerializeField] private GameObject ConnectPanel;
    [SerializeField] private InputField PlayerNameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartButton;

    private void Awake() {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    private void Start() {
        PlayerNameMenu.SetActive(true);
    }

    private void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected!");
    }

    public void ChangePlayerNameInput() {
        if (PlayerNameInput.text.Length >= 1) {
            StartButton.SetActive(true);
        }
        else {
            StartButton.SetActive(false);
        }
    }

    public void SetPlayerName() {
        PlayerNameMenu.SetActive(false);
        PhotonNetwork.playerName = PlayerNameInput.text;
    }

    public void CreateGame() {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { maxPlayers = 6 }, null);
    }

    public void JoinGame() {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 6;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);

    }

    private void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("MainGame");
    }
}
