using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamPlayer : MonoBehaviour
{
  public PhotonView photonView;

  public GameObject deathObject;
  public PlayerInfo playerInfo;
  bool isDead;

  public bool isMyTurn
  {
    get
    {
      return TurnController.GetPlayerTurn(this);
    }
  }

  protected virtual void Awake()
  {
    photonView = GetComponent<PhotonView>();
    playerInfo = GetComponent<PlayerInfo>();
  }

  protected void OnDestroy()
  {
    Instantiate(deathObject, transform.position, transform.rotation);

    TurnController.Remove(this);

    if(isDead)
    {
      return;
    }
    isDead = true;
    var newExplosion = PhotonNetwork.Instantiate("Explosion3", transform.position, Quaternion.identity, 0);
    newExplosion.transform.localScale = Vector3.one * .5f;
  }
}
