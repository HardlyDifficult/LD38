using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : MonoBehaviour
{
    public AudioClip deathMusic;
    public Text text;

    protected void OnEnable()
    {
        if (Camera.main == null)
        {
            return;
        }

        if (TurnController.WinningTeam == null)
        {
            text.text = "A draw!?";
        }
        else
        {
            text.text = TurnController.WinningTeam.TeamName;
        }
   
        var audio = Camera.main.GetComponent<AudioSource>();
        audio.clip = deathMusic;
        audio.Play();
    }
}
