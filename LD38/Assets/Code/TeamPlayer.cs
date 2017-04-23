using UnityEngine;

public class TeamPlayer : MonoBehaviour
{
    private Player _playerComponent;
    protected ExplosionDamage explosion;

    protected virtual void Awake()
    {
        explosion = Resources.Load<ExplosionDamage>("Explosion");
        _playerComponent = GetComponent<Player>();
    }

    protected void OnDestroy()
    {
        TurnController.Remove(_playerComponent);

        var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        newExplosion.transform.localScale = Vector3.one * .5f;
    }
}
