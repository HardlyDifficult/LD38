using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamPlayer : MonoBehaviour
{
  public GameObject deathObject;
  public PlayerInfo playerInfo;
  protected ExplosionDamage explosion;

  public bool isMyTurn
  {
    get
    {
      return TurnController.GetPlayerTurn(this);
    }
  }

  protected virtual void Awake()
  {
    explosion = Resources.Load<ExplosionDamage>("Explosion");
    playerInfo = GetComponent<PlayerInfo>();
  }

  protected void OnDestroy()
  {
    Instantiate(deathObject, transform.position, transform.rotation);

    TurnController.Remove(this);

    var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
    newExplosion.transform.localScale = Vector3.one * .5f;
  }
}
