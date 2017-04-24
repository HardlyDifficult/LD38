using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.PunBehaviour
{
  const string VERSION = "0.2";

  #region Public Data
  public string roomName = "Let's Play Grubs!";

  public int maxPlayersInRoom = 6;

  public InputField userNameField;

  public GameObject networkWorm;
  

  public Vector3 SpawnLocation
  {
    get
    {
      float x = UnityEngine.Random.Range(-10, 10);
      if(x < 0)
      {
        x -= 30;
      }
      else
      {
        x += 30;
      }
      float y = UnityEngine.Random.Range(-10, 10);
      if(y < 0)
      {
        y -= 30;
      }
      else
      {
        y += 30;
      }
      float z = UnityEngine.Random.Range(-10, 10);
      if(z < 0)
      {
        z -= 30;
      }
      else
      {
        z += 30;
      }

      return new Vector3(x, y, z);
    }
  }
  #endregion

  #region Private Data
  private bool _isInRoom = false;
  private int _teamID = 0;
  #endregion

  #region Public API
  void Start()
  {

    PhotonNetwork.isMessageQueueRunning = false;
    DontDestroyOnLoad(gameObject);

    PhotonNetwork.autoJoinLobby = false;
    PhotonNetwork.automaticallySyncScene = true;

    Connect();

    SceneManager.sceneLoaded += OnFinishedLoadingLevel;

    GameObject canvas = GameObject.Find("Canvas");
    canvas.transform.FindChild("ButtonHolder").FindChild("JoinRandomRoom").GetComponent<Button>().interactable = false;
  }

  private void OnDestroy()
  {
    PhotonNetwork.Disconnect();
    SceneManager.sceneLoaded -= OnFinishedLoadingLevel;
  }

  private void OnFinishedLoadingLevel(Scene scene, LoadSceneMode mode)
  {
    if(scene.name == "MainMultiplayer")
    {
      // if this is the main level
      if(_isInRoom)
      {
        GameObject canvas = GameObject.Find("Canvas_Ingame"); // get the canvas object

        canvas.transform.FindChild("MultiplayerPanel").FindChild("ServerText").GetComponent<Text>().text = "Connected to server as: " + PhotonNetwork.playerName;

        // Spawn network worm
        TeamPlayer worm = PhotonNetwork.Instantiate("NetworkWorm", SpawnLocation, Quaternion.identity, 0).GetComponent<TeamPlayer>();
        worm.GetComponentInChildren<NameDisplayer>().SetName(PhotonNetwork.playerName);
        TurnController.AddPlayer(_teamID, worm.GetComponent<PhotonView>().viewID);
      }
    }
    else
    {
      Destroy(gameObject);
    }

    PhotonNetwork.isMessageQueueRunning = true;
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

    _teamID = GameObject.Find("Canvas").transform.FindChild("PlayerInfo").FindChild("TeamOption").GetComponent<Dropdown>().value;

    PhotonNetwork.JoinRandomRoom();
  }

  public override void OnConnectedToMaster()
  {
    Debug.Log("Connected to master server!");
    GameObject canvas = GameObject.Find("Canvas");
    canvas.transform.FindChild("ButtonHolder").FindChild("JoinRandomRoom").GetComponent<Button>().interactable = true;
  }

  public override void OnJoinedRoom()
  {
    Debug.Log("Joined room!");

    // Load level
    PhotonNetwork.LoadLevel("MainMultiplayer");

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
