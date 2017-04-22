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
}
