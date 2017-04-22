using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnController : MonoBehaviour
{
  static TurnController instance;
  public enum Phase
  {
    Shoot, Hide
  }
  public static Phase phase;

  public GameObject[] wormList;

  public static event Action onTurnChange;

  public static GameObject currentWorm
  {
    get
    {
      return instance.wormList[currentTeam]; // TODO team plus worm..
    }
  }

  static int _currentTeam;
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

  protected void Awake()
  {
    instance = this;
  }

  protected void FixedUpdate()
  {
    timeRemaining--;
    if(timeRemaining <= 0)
    {
      currentTeam++;
    }
  }

  internal static void NextPhase()
  {
    phase++;
    timeRemaining = timeForPostTurn;
  }
}
