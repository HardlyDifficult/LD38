using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPlayer : MonoBehaviour {
  protected ExplosionDamage explosion;

  protected virtual void Awake()
  {
    explosion = Resources.Load<ExplosionDamage>("Explosion");
  }

  public int teamId;

  public bool isMyTurn
  {
    get
    {
      return teamId == TurnController.currentTeam;
    }
  }

  protected void Start()
  {
    TurnController.Add(this);
  }

  protected void OnDestroy()
  {
    TurnController.Remove(this);

    var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
    newExplosion.transform.localScale = Vector3.one * .5f;
  }
}
