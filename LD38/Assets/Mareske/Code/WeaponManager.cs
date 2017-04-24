using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
  //[Header("Game Design")]
  //We store our weapons here in the List
  public List<WeaponBlueprint> weaponList = new List<WeaponBlueprint>();

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
    if(PhotonNetwork.isMasterClient == false)
    {
      return;
    }

    //We go trough our weapon list
    foreach(WeaponBlueprint wb in weaponList)
    {
      //and Instantiate the weapon
      wb.weaponInstance = PhotonNetwork.Instantiate(wb.weaponPrefab, this.transform.position, Quaternion.identity, 0);

      me.GetComponent<PhotonView>().RPC("RpcShowWeapon", PhotonTargets.AllBuffered, wb.weaponID, wb.weaponInstance.GetComponent<PhotonView>().viewID);

      //after that we instantiate the UI
      
    }

    //Then we hide all the weapons
    DeactivateWeapons();
  }

  [PunRPC]
  void RpcShowWeapon(int weaponId, int viewId)
  {

    for(int i = 0; i < me.weaponList.Count; i++)
    {
      if(me.weaponList[i].weaponID == weaponId)
      {
        me.weaponList[i].weaponInstance = PhotonView.Find(viewId).gameObject;
      }
    }

    WeaponSelectionGrid.me.InstantiateWeaponButton(weaponId, viewId);
 
  }

  internal static Sprite GetIcon(int weaponId)
  {
    for(int i = 0; i < me.weaponList.Count; i++)
    {
      if(me.weaponList[i].weaponID == weaponId)
      {
        return me.weaponList[i].weaponIcon;
      }
    }

    return null;
  }

  /// <summary>
  /// Deactivates the other weapons and activates the weapon with the given ID
  /// </summary>
  /// <param name="_id"></param>
  public void ActivateWeapon(int _id)
  {
    GetComponent<PhotonView>().RPC("DoActivate", PhotonTargets.AllBuffered, 
      new[] { _id, TurnController.instance.currentTeamId, TurnController.CurrentTeam._currentPlayerIndex });
  }
  //var id = new[] { _id, TurnController.currentTeamId, TurnController.CurrentTeam._currentPlayerIndex };
  [PunRPC]
  void DoActivate(int[] id)
  {
    //Deactivating all weapons, why all? Just in case something weird happens
    DeactivateWeapons();
    int _id = (int)id[0];
    var curWorm_WeaponTrans = TurnController.GetPlayer((int)id[1], (int)id[2]).playerInfo.weaponMountPosition;

    if(_id == 3)
    {
      var go = PhotonNetwork.Instantiate(weaponList[_id].weaponPrefab, 
        curWorm_WeaponTrans.position + curWorm_WeaponTrans.rotation * Quaternion.Euler(0,90,0) * new Vector3(5, 0, 0), 
        curWorm_WeaponTrans.rotation, 0);
      // TODO end turn
      return;
    }


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
    if(PhotonNetwork.isMasterClient == false)
    {
      return;
    }

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
  public Sprite weaponIcon;
  public string weaponPrefab;
  [HideInInspector]
  public GameObject weaponInstance;
}
