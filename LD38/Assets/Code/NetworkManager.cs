using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.PunBehaviour
{
    const string VERSION = "0.1";

    #region Public Data
    public string roomName = "Let's Play Grubs!";

    public int maxPlayersInRoom = 6;

    public InputField userNameField;
    #endregion

    #region Private Data
    private bool _isInRoom = false;
    #endregion

    #region Public API
    void Start ()
    {
        DontDestroyOnLoad(gameObject);

        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;

        Connect();

        SceneManager.sceneLoaded += OnFinishedLoadingLevel;
	}
	
	void Update ()
    {
		
	}

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnFinishedLoadingLevel;
    }

    private void OnFinishedLoadingLevel(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Main")
        {
            // if this is the main level
            if(_isInRoom)
            {
                GameObject canvas = GameObject.Find("Canvas_Ingame"); // get the canvas object

                canvas.transform.FindChild("MultiplayerPanel").FindChild("ServerText").GetComponent<Text>().text = "Connected to server as: " + PhotonNetwork.playerName;
            }
        }
    }

    public void BackToMainMenu()
    {
        SoundManager.PlayClick();

        PhotonNetwork.Disconnect();

        SceneManager.LoadScene("MainMenu");

        Destroy(gameObject);
    }
    #endregion

    #region Network Events
    public void Connect()
    {
        if(!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }
    }

    public void JoinGame()
    {
        SoundManager.PlayClick();

        PhotonNetwork.playerName = userNameField.text;

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server!");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room!");

        // Load level
        PhotonNetwork.LoadLevel("Main");

        _isInRoom = true;
    }

    public override void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("Disconnected from Photon!");
    }

    public void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No random room available, creating one");

        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = (byte)maxPlayersInRoom }, null);
    }
    #endregion
}
