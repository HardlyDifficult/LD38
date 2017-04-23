using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : MonoBehaviour
{
  public AudioClip deathMusic;
  public Text text;

  protected void OnEnable()
  {
    if(Camera.main == null)
    {
      return;
    }

    text.text = "Team " + (TurnController.winningTeamId + 1);
    var audio = Camera.main.GetComponent<AudioSource>();
    audio.clip = deathMusic;
    audio.Play();
  }
}
