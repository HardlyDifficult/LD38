using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonReturnToMenu : MonoBehaviour {

	public void OnClick()
  {
    SceneManager.LoadScene("MainMenu");
  }
}
