using UnityEngine;
using System.Collections.Generic;

public class Team
{
  /// <summary>
  /// TODO sync
  /// </summary>
  public int _currentPlayerIndex = 0;
  
  public int Id;
  public string TeamName;

  public List<TeamPlayer> playerList;

  public float TeamHealth
  {
    get
    {
      float health = 0;
      for(int i = 0; i < playerList.Count; i++)
      {
        if(playerList[i] != null)
        {
          health += playerList[i].playerInfo.Health;
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

  public TeamPlayer CurrentPlayer
  {
    get
    {
      if(playerList.Count == 0)
      {
        return null;
      }

      for(int i = 0; i < playerList.Count; i++)
      {
        if(playerList[i].photonView.viewID == _currentPlayerIndex)
        {
          return playerList[i];
        }
      }

      return null;
    }
  }


  public Team(int id, int teamSize, string teamName)
  {
    Id = id;
    TeamName = teamName;

    playerList = new List<TeamPlayer>();

    TurnController.onTurnChange += OnTurnChange;
  }

  private void OnTurnChange()
  {
    if(TurnController.instance.currentTeamId == Id)
    {
      CycleTeamMembers();
    }
  }

  private void CycleTeamMembers()
  {
    if(!TeamAlive) return;

    int targetI = 0;
    for(int i = 0; i < playerList.Count; i++)
    {
      if(playerList[i].photonView.viewID == _currentPlayerIndex)
      {
        targetI = i;
      }
    }
    targetI++;
    if(targetI >= playerList.Count)
    {
      targetI = 0;
    }

    _currentPlayerIndex = playerList[targetI].photonView.viewID;

  }

  public void AddPlayer(TeamPlayer player)
  {
    if(!ContainsPlayer(player))
    {
      playerList.Add(player);
    }
  }

  public void RemovePlayer(TeamPlayer player)
  {
    playerList.Remove(player);
  }

  public bool ContainsPlayer(TeamPlayer p)
  {
    return playerList.Contains(p);
  }


  public bool GetTurn(TeamPlayer player)
  {
    return CurrentPlayer == player;
  }
}
