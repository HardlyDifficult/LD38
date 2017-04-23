
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Phase
{
  Shoot, Hide
}


public class TurnController : MonoBehaviour
{
  public GameObject gameOverPanel;

  public static bool isGameOver;
  public static TurnController instance;
  public static Phase phase;

  public List<Team> teamList;

  public static event Action onTurnChange;


  public static int playersPerTeam = 10;

  const int timeForPreTurn = 1000;
  const int timeForPostTurn = timeForPreTurn / 10;
  public static int timeRemaining;

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
      if(_currentTeamId >= instance.teamList.Count)
        return null;

      return instance.teamList[_currentTeamId];
    }
  }


  private static int _currentTeamId;
  public static int currentTeamId
  {
    get
    {
      return _currentTeamId;
    }
    set
    {
      if(instance.teamList.Count == 0)
      {
        return;
      }

      phase = 0;
      _currentTeamId = value % instance.teamList.Count;
      if(onTurnChange != null)
      {
        onTurnChange.Invoke();
      }

      timeRemaining = timeForPreTurn;
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
      instance.teamList.Add(new Team(instance.teamList.Count, 0, instance.teamList.Count.ToString())); // TODO name
    }

    return instance.teamList[teamID];
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
    phase = Phase.Shoot;
    isGameOver = false;
    instance = this;

    teamList = new List<Team>();
  }

  protected void FixedUpdate()
  {
    if(isGameOver) return;

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

  public static void AddTeam(Team team)
  {
    if(!instance.teamList.Contains(team))
      instance.teamList.Add(team);
  }

  public static void AddPlayer(Team team, TeamPlayer player)
  {
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
    phase++;
    timeRemaining = timeForPostTurn;
  }
  #endregion
}
