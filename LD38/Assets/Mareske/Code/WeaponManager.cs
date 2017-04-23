using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

  [Header("Game Design")]
  //We store our weapons here in the List
  public List<WeaponBlueprint> weaponList = new List<WeaponBlueprint>();

  public Transform curWorm_WeaponTrans
  {
    get
    {
      return TurnController.CurrentPlayer.weaponMountPosition;
    }
  }

  public static WeaponManager me;

  private void Awake()
  {
    me = this;

    for(int i = 0; i < weaponList.Count; i++)
    {
      weaponList[i].weaponID = i;
    }
  }

  private void Start()
  {
    SetUpWeapons();
  }

  /// <summary>
  /// Instantiates the Weapons and parents them to this transform
  /// </summary>
  public void SetUpWeapons()
  {
    //We go trough our weapon list
    foreach(WeaponBlueprint wb in weaponList)
    {
      //and Instantiate the weapon
      wb.weaponInstance = Instantiate(wb.weaponPrefab, this.transform.position, Quaternion.identity);

      //after that we instantiate the UI
      WeaponSelectionGrid.me.InstantiateWeaponButton(wb);
    }

    //Then we hide all the weapons
    DeactivateWeapons();
  }

  /// <summary>
  /// Deactivates the other weapons and activates the weapon with the given ID
  /// </summary>
  /// <param name="_id"></param>
  public void ActivateWeapon(int _id)
  {
    //Deactivating all weapons, why all? Just in case something weird happens
    DeactivateWeapons();

    //We move and activate the weapon that we need
    weaponList[_id].weaponInstance.transform.position = curWorm_WeaponTrans.position;
    weaponList[_id].weaponInstance.transform.rotation = curWorm_WeaponTrans.rotation;
    weaponList[_id].weaponInstance.transform.SetParent(curWorm_WeaponTrans);
    weaponList[_id].weaponInstance.SetActive(true);
  }

  /// <summary>
  /// We deactivate all the weapons
  /// </summary>
  public void DeactivateWeapons()
  {
    //We go trought each weaponBlueprint
    foreach(WeaponBlueprint wb in weaponList)
    {
      //We set the parent to keep the scene clean and find it easier for debuuging and then hide it
      wb.weaponInstance.transform.SetParent(this.transform);
      wb.weaponInstance.SetActive(false);
    }
  }
}

[System.Serializable]
public class WeaponBlueprint
{
  [Header("Data")]
  public string weaponName;
  public int weaponID;
  public float weaponDamage;

  [Header("Referenzes")]
  public Image weaponIcon;
  public GameObject weaponPrefab;
  [HideInInspector]
  public GameObject weaponInstance;
}
