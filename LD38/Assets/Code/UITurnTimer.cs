using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurnTimer : MonoBehaviour
{
  Text text;

  protected void Start()
  {
    text = GetComponent<Text>();
  }

  public void Update()
  {
    if(!TurnController.isGameOver)
    {
      text.text = Mathf.RoundToInt(TurnController.instance.timeRemaining * Time.fixedDeltaTime).ToString();
    }
  }
}
