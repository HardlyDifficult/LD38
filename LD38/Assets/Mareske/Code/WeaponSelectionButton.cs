using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponSelectionButton : MonoBehaviour
{

  [Header("Data")]
  public int weaponID;

  [Header("UI")]
  public Image weaponIcon;

  /// <summary>
  /// Activates the Weapon
  /// </summary>
  public void SelectWeapon()
  {
    if(TurnController.CurrentPlayer.GetComponent<PhotonView>().isMine == false)
    {
      return;
    }

    WeaponManager.me.ActivateWeapon(weaponID);
  }
}
