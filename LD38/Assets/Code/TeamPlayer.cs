using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPlayer : MonoBehaviour {

  public int teamId;

  public bool isMyTurn
  {
    get
    {
      return teamId == TurnController.currentTeam;
    }
  }

  protected void Start()
  {
    TurnController.Add(this);
  }

  protected void OnDestroy()
  {
    TurnController.Remove(this);
  }
}
