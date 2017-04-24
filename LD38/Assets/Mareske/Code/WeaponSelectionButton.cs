using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponSelectionButton : MonoBehaviour
{

  [Header("Data")]
  public int weaponID;

   public Sprite weaponIcon
  {
    set
    {
      GetComponent<Image>().sprite = value;
    }
  }

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
