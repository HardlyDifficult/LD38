using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFirePower : MonoBehaviour
{
  Shoot shooter;
  Image image;
  // Use this for initialization
  void Start()
  {
    image = GetComponent<Image>();
  }

  // Update is called once per frame
  void Update()
  {
    shooter = transform.root.GetComponentInChildren<Shoot>();
    if(shooter == null)
    {
      return;
    }
    if(shooter.showShootPower)
    {
    image.fillAmount = shooter.shootPower;
    } else
    {
      image.fillAmount = 0;
    }

    //print(shooter.shootPower + " power " + shooter.shootHoldTime);
  }
}
