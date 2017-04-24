using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
  public Transform weaponMountPosition;

  public UILifeLine uiLifeLineComponent;
  public LifeLine LifeLineComponent
  {
    get
    {
      return uiLifeLineComponent.life;
    }
  }


  public float Health
  {
    get { return LifeLineComponent.life; }
  }

  public string PlayerName = "Jane doe";

  public Team team;

}

