
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Phase
{
  Shoot, Hide
}

public class TurnController : MonoBehaviour
{
  /// <summary>
  /// TODO - sync
  /// </summary>
  Phase _phase;
  int _timeRemaining;
  int _currentTeamId;

  public Phase phase
  {
    get
    {
      return _phase;
    }
    set
    {
      if(WeaponManager.me != null)
      {
        WeaponManager.me.DeactivateWeapons();
      }
      instance.GetComponent<PhotonView>().RPC("SetPhase", PhotonTargets.AllBuffered, value);
    }
  }

  [PunRPC]
  void SetPhase(Phase value)
  {
    _phase = value;
  }

  public int timeRemaining
  {
    get
    {
      return _timeRemaining;
    }
    set
    {
      instance.GetComponent<PhotonView>().RPC("SetTimeRemaining", PhotonTargets.All, value);
    }
  }

  [PunRPC]
  void SetTimeRemaining(int value)
  {
    _timeRemaining = value;
  }

  public int currentTeamId
  {
    get
    {
      return _currentTeamId;
    }
    set
    {
      instance.GetComponent<PhotonView>().RPC("SetTeamId", PhotonTargets.AllBuffered, value);
    }
  }

  [PunRPC]
  void SetTeamId(int value)
  {
    if(instance.teamList.Count == 0)
    {
      return;
    }

    instance.phase = 0;
    instance._currentTeamId = value % instance.teamList.Count;
    if(onTurnChange != null)
    {
      onTurnChange.Invoke();
    }

    instance.timeRemaining = timeForPreTurn;
  }

  public GameObject gameOverPanel;

  public static bool isGameOver;
  public static TurnController instance;

  public List<Team> teamList;

  public static event Action onTurnChange;

  public static int playersPerTeam = 10;

  const int timeForPreTurn = 1000;
  const int timeForPostTurn = timeForPreTurn / 10;

  internal static IEnumerable<TeamPlayer> GetAllPlayers()
  {
    foreach(var team in instance.teamList)
    {
      foreach(var player in team.playerList)
      {
        yield return player;
      }
    }
  }

  public static Team CurrentTeam
  {
    get
    {
      if(instance.currentTeamId >= instance.teamList.Count)
        return null;

      return instance.teamList[instance.currentTeamId];
    }
  }

  
  public static bool HasPlayer
  {
    get { return CurrentPlayer != null; }
  }

  public static PlayerInfo CurrentPlayer
  {
    get
    {
      if(CurrentTeam == null || CurrentTeam.CurrentPlayer == null)
      {
        return null;
      }

      return CurrentTeam.CurrentPlayer.playerInfo;
    }
  }

  internal static Team GetTeam(int teamID)
  {
    while(instance.teamList.Count <= teamID)
    {
      instance.teamList.Add(new Team(instance.teamList.Count, 0, teamID == 0 ? "Team Red" : "Team Blue",
        teamID == 0 ? new Color(255,0,0,1) : new Color(0,0,255,1)
        )); // TODO name
    }

    return instance.teamList[teamID];
  }

  internal static TeamPlayer GetPlayer(int teamId, int playerId)
  {
    var team = instance.teamList[teamId];
    for(int i = 0; i < team.playerList.Count; i++)
    {
      if(team.playerList[i].photonView.viewID == playerId)
      {
        return team.playerList[i];
      }
    }

    return null;
  }

  public static Team WinningTeam
  {
    get
    {
      float bestTeamHealth = 0.0f;
      Team teamWithBestHealth = null;
      for(int i = 0; i < instance.teamList.Count; i++)
      {
        Team t = instance.teamList[i];
        if(t.TeamHealth > bestTeamHealth)
        {
          bestTeamHealth = t.TeamHealth;
          teamWithBestHealth = t;
        }

      }

      return teamWithBestHealth;
    }
  }

  #region Events

  protected void OnEnable()
  {
    instance = this;
    phase = Phase.Shoot;
    isGameOver = false;

    teamList = new List<Team>();
  }

  protected void FixedUpdate()
  {
    if(isGameOver) return;

    if(PhotonNetwork.isMasterClient == false)
    {
      return;
    }

    if(Input.GetKeyDown(KeyCode.T))
      timeRemaining = 0;

    if(CurrentPlayer == null)
      timeRemaining = 0;

    timeRemaining--;
    if(timeRemaining <= 0)
    {
      currentTeamId++;
    }
  }

  protected void OnDestroy()
  {
    isGameOver = true;
  }
  #endregion

  #region API
  

  public static void AddPlayer(int teamId, int playerViewId)
  {
    instance.GetComponent<PhotonView>().RPC("DoAddPlayer", PhotonTargets.AllBuffered, teamId, playerViewId);
  }

  [PunRPC]
  void DoAddPlayer(int teamId, int playerViewId) {
    var team = TurnController.GetTeam(teamId);
    var player = PhotonView.Find(playerViewId).GetComponent<TeamPlayer>();
    team.AddPlayer(player);
  }

  public static Team FindPlayerTeam(TeamPlayer p)
  {
    for(int i = 0; i < instance.teamList.Count; i++)
    {
      if(instance.teamList[i].ContainsPlayer(p))
      {
        return instance.teamList[i];
      }
    }

    return null;
  }

  public static bool GetPlayerTurn(TeamPlayer playerObj)
  {
    return CurrentTeam.GetTurn(playerObj);
  }

  public static void Remove(
    TeamPlayer player)
  {
    if(isGameOver)
    {
      return;
    }

    Team t = FindPlayerTeam(player);
    t.RemovePlayer(player);

    if(!t.TeamAlive)
    {
      isGameOver = true;
      if(instance.gameOverPanel != null)
      {
        instance.gameOverPanel.SetActive(true);
      }
    }
  }

  internal static void NextPhase()
  {
    instance.phase++;
    instance.timeRemaining = timeForPostTurn;
  }
  #endregion
}
