using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

  [Header("References")]
  public GameObject optionsScreenUI;
  public Slider soundVolumeSliderUI;
  public Slider musicVolumeSliderUI;


    #region Buttons
    /// <summary>
    /// Loads the game scene
    /// </summary>
    public void StartGame()
  {
        //TODO: Hook this backup
    //SoundManager.PlayClick();

    optionsScreenUI.SetActive(false);
        //if (!IsMultiplayer)
        //    SceneManager.LoadScene("Main");
        //else
            SceneManager.LoadScene("MultiplayerLobby");
  }

  /// <summary>
  /// Loads the credits scene
  /// </summary>
  public void GoToCredits()
  {
        //TODO: Hook this backup
        //SoundManager.PlayClick();

        optionsScreenUI.SetActive(false);
    SceneManager.LoadScene("Credits");
  }

  /// <summary>
  /// Quits the application
  /// </summary>
  public void QuitGame()
  {
        //TODO: Hook this backup
        //SoundManager.PlayClick();

        optionsScreenUI.SetActive(false);
    Application.Quit();
  }

  public void OpenOptionsScreen()
  {
    soundVolumeSliderUI.value = Game.SoundManager.EffectVolume;
    musicVolumeSliderUI.value = Game.SoundManager.MusicVolume;

        //TODO: Hook this backup
        //SoundManager.PlayClick();
    optionsScreenUI.SetActive(true);
  }
  #endregion

  #region Options
  /// <summary>
  /// Gets the sound volume slider change and gives the new volume to the soundmanager
  /// </summary>
  /// <param name="_volume"></param>
  public void SoundVolumeChange()
  {
        Game.SoundManager.EffectVolume = soundVolumeSliderUI.value;
  }

  /// <summary>
  /// Gets the music volume slider change and gives the new volume to the soundmanager
  /// </summary>
  /// <param name="_volume"></param>
  public void MusicVolumeChange()
  {
        Game.SoundManager.MusicVolume = musicVolumeSliderUI.value;
  }
  #endregion
}
