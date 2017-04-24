using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Shoot : MonoBehaviour
{
  public TeamPlayer Player;
  protected Transform bulletSpawnAnchorPointOnGun;

  const float maxHoldTime = 2;
  public float shootHoldTime;

  public abstract bool showShootPower
  {
    get;
  }

  public virtual float shootPower
  {
    get
    {
      var _shootHoldTime = shootHoldTime % (maxHoldTime * 2);
      if(_shootHoldTime > maxHoldTime)
      {
        _shootHoldTime = maxHoldTime * 2 - _shootHoldTime;
      }
      return _shootHoldTime / maxHoldTime;
    }
  }

  protected virtual void Start()
  {
    bulletSpawnAnchorPointOnGun = transform.GetChild(0).FindChild("BulletSpawn");
  }

  protected virtual void OnFireStart()
  {

  }

  protected virtual void OnFireStay()
  {

  }

  protected virtual void OnFireStop()
  {

  }

  protected void FixedUpdate()
  {
    Player = GetComponentInParent<TeamPlayer>();

    if(Player == null || Player.GetComponent<PhotonView>().isMine == false)
    {
      return;
    }

    if(!TurnController.GetPlayerTurn(Player) || TurnController.instance.phase != Phase.Shoot)
    {
      return;
    }

    Aim();

    if(Input.GetAxis("Fire1") > 0 && EventSystem.current.IsPointerOverGameObject() == false)
    {
      if(shootHoldTime == 0)
      {
        OnFireStart();
      }
      OnFireStay();
      shootHoldTime += Time.deltaTime;
    }
    else
    {
      OnFireStop();
      shootHoldTime = 0;
    }
  }

  void Aim()
  {
    Vector2 screenPos = Input.mousePosition;
    if(screenPos.x < Screen.width / 2)
    {
      screenPos += new Vector2(Screen.width / 2, 0);
    }

    Ray targetRay = Camera.main.ScreenPointToRay(screenPos);
    Plane plane = new Plane(transform.root.forward, transform.root.position);

    float distance;
    if(plane.Raycast(targetRay, out distance))
    {
      Vector3 mousePosition = targetRay.GetPoint(distance);

      Vector3 delta = transform.position - mousePosition;
      Vector3 up = transform.position - Vector3.zero;

      Quaternion originalRotation = transform.rotation;
      Quaternion targetRotation = Quaternion.LookRotation(delta, up);


      //Vector3 euler = transform.localRotation.eulerAngles;
      if((transform.right - transform.root.forward).sqrMagnitude > 1
        //Mathf.Abs(euler.y) > 1 || Mathf.Abs(euler.z) > 1 || 
        //euler.x > 75 || euler.x < -20
        )
      {
        targetRotation = originalRotation;
      }

      GetComponent<PhotonView>().RPC("SetRotation", PhotonTargets.All, targetRotation);
    }
  }

  [PunRPC]
  public void SetRotation(Quaternion targetRotation)
  {
    if(PhotonView.Get(this).isMine)
    {
      transform.rotation = targetRotation;
    }
  }

  /// <param name="antiAccurancyInDegrees">Higher means worse aim</param>
  protected void FireProjectile(string resource, float antiAccurancyInDegrees)
  {
    Quaternion rng = Quaternion.Euler(UnityEngine.Random.Range(-antiAccurancyInDegrees, antiAccurancyInDegrees),
      UnityEngine.Random.Range(-antiAccurancyInDegrees, antiAccurancyInDegrees),
      UnityEngine.Random.Range(-antiAccurancyInDegrees, antiAccurancyInDegrees));
    Projectile newBullet = PhotonNetwork.Instantiate(resource, bulletSpawnAnchorPointOnGun.transform.position, transform.rotation * rng, 0).GetComponent<Projectile>();
    newBullet.shooter = gameObject.transform.root.gameObject;
    newBullet.shootPower = shootPower;
  }
}
