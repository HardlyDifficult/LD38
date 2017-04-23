using UnityEngine;

public class TeamPlayer : MonoBehaviour
{
  private PlayerInfo _playerComponent;
  protected ExplosionDamage explosion;

  public bool isMyTurn
  {
    get
    {
      return TurnController.GetPlayerTurn(_playerComponent);
    }
  }

  protected virtual void Awake()
  {
    explosion = Resources.Load<ExplosionDamage>("Explosion");
    _playerComponent = GetComponent<PlayerInfo>();
  }

  protected void OnDestroy()
  {
    TurnController.Remove(_playerComponent);

    var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
    newExplosion.transform.localScale = Vector3.one * .5f;
  }
}
