using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject[] PlayerPrefabs;
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        var skinNum = Random.Range(0,PlayerPrefabs.Length);
        PhotonNetwork.Instantiate(PlayerPrefabs[skinNum].name, pos, Quaternion.identity);
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.LogFormat("Player {0} lefy room", otherPlayer.NickName);
    }
}