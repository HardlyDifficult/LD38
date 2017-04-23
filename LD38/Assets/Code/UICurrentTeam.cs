using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurrentTeam : MonoBehaviour {
  Text text;

  protected void Start()
  {
    text = GetComponent<Text>();
    Debug.Assert(text != null);

    TurnController.onTurnChange += TurnController_onTurnChange;
  }

  protected void OnDestroy()
  {
    TurnController.onTurnChange -= TurnController_onTurnChange;
  }

  private void TurnController_onTurnChange()
  {
    switch(TurnController.currentTeam)
    {
      default:
      case 0:
        text.text = "Team 1";
        break;
      case 1:
        text.text = "Team 2";
        break;
    }
  }
}
