using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TurnController : MonoBehaviour
{
  bool isGameOver;
  static TurnController instance;
  public enum Phase
  {
    Shoot, Hide
  }
  public static Phase phase;

  List<TeamPlayer>[] wormList;

  public static event Action onTurnChange;

  public static TeamPlayer currentWorm
  {
    get
    {
      if(instance.wormList[currentTeam].Count == 0)
      {
        return null;
      }

      return instance.wormList[currentTeam][currentPlayer];
    }
  }

  static int _currentTeam;
  static int currentPlayer; // TODO a way to change player
  public static int teamCount = 2;

  const int timeForPreTurn = 1000;
  const int timeForPostTurn = timeForPreTurn / 10;
  public static int timeRemaining;

  public static int currentTeam
  {
    get
    {
      return _currentTeam;
    }
    set
    {
      phase = 0;
      _currentTeam = value % teamCount;
      if(onTurnChange != null)
      {
        onTurnChange.Invoke();
      }

      timeRemaining = timeForPreTurn;
    }
  }

  #region Events
 
  protected void OnEnable()
  {
    isGameOver = false;
    instance = this;
    wormList = new List<TeamPlayer>[2];
    for(int i = 0; i < wormList.Length; i++)
    {
      wormList[i] = new List<TeamPlayer>();
    }
  }

  protected void FixedUpdate()
  {
    timeRemaining--;
    if(timeRemaining <= 0)
    {
      currentTeam++;
    }
  }
  #endregion

  #region API
  public static void Add(
    TeamPlayer player)
  {
    instance.wormList[player.teamId].Add(player);
  }

  public static void Remove(
    TeamPlayer player)
  {
    if(instance.isGameOver)
    {
      return;
    }

    instance.wormList[player.teamId].Remove(player);

    if(instance.wormList[player.teamId].Count == 0)
    { // Game over, you lose.
      instance.isGameOver = true;
      SceneManager.LoadScene("MainMenu");
    }
  }

  internal static void NextPhase()
  {
    phase++;
    timeRemaining = timeForPostTurn;
  }
  #endregion
}
