using UnityEngine;
using UnityEngine.UI;

public class UICurrentTeam : MonoBehaviour
{
  public Text CurrentTeamText;
  public Text CurrentWormText;

  private void FixedUpdate()
  {
    if(TurnController.isGameOver)
    {
      gameObject.SetActive(false);
      return;
    }

    if(TurnController.CurrentTeam != null)
    {
      CurrentTeamText.text = "Team " + TurnController.CurrentTeam.Id;//TurnController.CurrentTeam.TeamName;
    }

    if(TurnController.CurrentPlayer != null)
    {
      CurrentWormText.text = "Player " + TurnController.CurrentTeam._currentPlayerIndex; //TurnController.CurrentPlayer.PlayerName;
    }
  }
}
