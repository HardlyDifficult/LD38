using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : MonoBehaviour
{
  public AudioClip deathMusic;
  public Text textWinningPlayerList;
  public Text textWinningTeamName;

  protected void OnEnable()
  {
    if(Camera.main == null)
    {
      return;
    }

    if(TurnController.WinningTeam == null)
    {
      textWinningPlayerList.text = "A draw!?";
    }
    else
    {
      textWinningTeamName.text = TurnController.WinningTeam.TeamName;
      string playerList = "";
      for(int i = 0; i < TurnController.WinningTeam.playerList.Count; i++)
      {
        if(i > 0)
        {
          playerList += " ,";
        }
        playerList += TurnController.WinningTeam.playerList[i].playerInfo.PlayerName;
      }
      textWinningPlayerList.text = playerList;
    }

    var audio = Camera.main.GetComponent<AudioSource>();
    audio.clip = deathMusic;
    audio.Play();
  }
}
