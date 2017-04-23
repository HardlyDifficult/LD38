using UnityEngine;
using System.Collections.Generic;

public class Team
{
  public int Id;
  public string TeamName;

  public List<PlayerInfo> playerList;

  public float TeamHealth
  {
    get
    {
      float health = 0;
      for(int i = 0; i < playerList.Count; i++)
      {
        if(playerList[i] != null)
        {
          health += playerList[i].Health;
        }
      }

      float teamMaxHealth = playerList.Count * 100.0f;

      return Mathf.Clamp01(health / teamMaxHealth);
    }
  }

  public bool TeamAlive
  {
    get { return TeamHealth > 0.0f; }
  }

  public PlayerInfo CurrentPlayer
  {
    get
    {
      if(playerList.Count == 0)
      {
        return null;
      }

      return playerList[_currentPlayerIndex];
    }
  }

  private int _currentPlayerIndex = 0;

  public Team(int id, int teamSize, string teamName)
  {
    Id = id;
    TeamName = teamName;

    playerList = new List<PlayerInfo>();

    TurnController.onTurnChange += OnTurnChange;
  }

  private void OnTurnChange()
  {
    if(TurnController.currentTeamId == Id)
    {
      CycleTeamMembers();
    }
  }

  private void CycleTeamMembers()
  {
    if(!TeamAlive) return;

    _currentPlayerIndex++;

    if(_currentPlayerIndex >= playerList.Count)
      _currentPlayerIndex = 0;

    if(playerList[_currentPlayerIndex] == null)
      CycleTeamMembers();

  }

  public void AddPlayer(PlayerInfo player)
  {
    if(!ContainsPlayer(player))
    {
      playerList.Add(player);
    }
  }

  public void RemovePlayer(PlayerInfo player)
  {
    playerList.Remove(player);
  }

  public bool ContainsPlayer(PlayerInfo p)
  {
    return playerList.Contains(p);
  }


  public bool GetTurn(PlayerInfo player)
  {
    return CurrentPlayer == player;
  }


}
