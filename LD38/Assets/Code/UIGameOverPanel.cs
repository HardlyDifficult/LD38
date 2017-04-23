using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : MonoBehaviour
{
  public Text text;

  protected void OnEnable()
  {
    text.text = "Team " + (TurnController.winningTeamId + 1);
  }
}
