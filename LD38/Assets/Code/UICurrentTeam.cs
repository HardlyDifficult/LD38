using UnityEngine;
using UnityEngine.UI;

public class UICurrentTeam : MonoBehaviour
{
    public Text CurrentTeamText;
    public Text CurrentWormText;

    private void FixedUpdate()
    {
        if (TurnController.isGameOver)
        {
            gameObject.SetActive(false);
            return;
        }

        CurrentTeamText.text = TurnController.CurrentTeam.TeamName;

        if(TurnController.CurrentPlayer != null)
            CurrentWormText.text = TurnController.CurrentPlayer.PlayerName;
    }
}
