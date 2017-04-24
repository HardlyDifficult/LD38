using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionGrid : MonoBehaviour {

    public GameObject weaponButtonUIPrefab;
    public static WeaponSelectionGrid me;

    private void Awake()
    {
        me = this;
    }

    /// <summary>
    /// Creates a Button for the weaponGrid so that we can select the weapon
    /// </summary>
    /// <param name="_id"></param>
    public void InstantiateWeaponButton(int weaponId, int weaponViewId)
    {
    
        //We instantiate the button
        GameObject go = Instantiate(weaponButtonUIPrefab, this.transform.position, this.transform.rotation) as GameObject;
        go.transform.SetParent(this.transform);

        //Then we set everything we need
        WeaponSelectionButton tempScript = go.GetComponent<WeaponSelectionButton>();
        tempScript.weaponID = weaponId;

    var weaponIcon = WeaponManager.GetIcon(weaponId);
        //If someone already made the weaponIcon and then we show it
        if (weaponIcon != null)
            tempScript.weaponIcon = weaponIcon;     
    }
}
