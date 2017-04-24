using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class IngameMenu : MonoBehaviour
{

  [Header("Data")]
  public bool inMenu = false;

  [Header("Referenzes")]
  public GameObject menuUI;
  public GameObject optionsUI;
  public Slider soundVolumeSliderUI;
  public Slider musicVolumeSliderUI;
  public GameObject weaponSelectionUI;


  private void Start()
  {
    soundVolumeSliderUI.value = SoundManager.soundVolume;
    musicVolumeSliderUI.value = SoundManager.musicVolume;
  }

  private void Update()
  {
    if(Input.GetKeyDown(KeyCode.Escape))
    {
      ToggleMenu();
    }
  }

  /// <summary>
  /// Toggles the Menu on and Off
  /// </summary>
  public void ToggleMenu()
  {
    //If we are in the Menu
    if(inMenu)
    {
      //Then we unpause the game
      inMenu = false;

      menuUI.SetActive(false);
      optionsUI.SetActive(false);
      weaponSelectionUI.SetActive(true);

    }
    else
    {
      //If not then we pause the Game
      inMenu = true;

      menuUI.SetActive(true);
      weaponSelectionUI.SetActive(false);

    }
  }

  /// <summary>
  /// Resumes the Game, by toggling the Menu, because normaly we should be in the menu when clicking resume
  /// </summary>
  public void Resume()
  {
    ToggleMenu();
  }

  /// <summary>
  /// Loads the Main Menu Scene
  /// </summary>
  public void ExitToMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }

  #region Options
  /// <summary>
  /// Gets the sound volume slider change and gives the new volume to the soundmanager
  /// </summary>
  /// <param name="_volume"></param>
  public void SoundVolumeChange()
  {
    SoundManager.soundVolume = soundVolumeSliderUI.value;
  }

  /// <summary>
  /// Gets the music volume slider change and gives the new volume to the soundmanager
  /// </summary>
  /// <param name="_volume"></param>
  public void MusicVolumeChange()
  {
    SoundManager.musicVolume = musicVolumeSliderUI.value;
  }
  #endregion
}
