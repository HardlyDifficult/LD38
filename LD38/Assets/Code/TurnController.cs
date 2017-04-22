using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnController : MonoBehaviour {

  public static event Action onTurnChange;

  static int _currentTeam;
  public static int teamCount = 2;

  const int timeForPreTurn = 1000;
  const int timeForPostTurn = 10;
  public static int timeRemaining;

  public static int currentTeam
  {
     get
    {
      return _currentTeam;
    }
    set
    {
      _currentTeam = value % teamCount;
      if(onTurnChange != null)
      {
        onTurnChange.Invoke();
      }

      timeRemaining = timeForPreTurn;
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
}
