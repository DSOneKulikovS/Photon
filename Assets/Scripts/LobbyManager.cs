using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text LogText;
    public GameObject Buttons;
    public GameObject CreateButton;

    void Start()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(1000, 9999);
        Log("Player Nick Name is set to " + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CreateButton.SetActive(!CreateButton.activeSelf);
        }
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
        Buttons.SetActive(true);
        checkServer();

    }

    void checkServer()
    {
        if (PhotonNetwork.CountOfRooms > 0)
        {
            Log("Server On");
        }
        else
        {
            Log("Server Off");
        }
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 20 });
    }

    public void JoinRoom()
    {
        checkServer();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel("Game");
    }

    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}