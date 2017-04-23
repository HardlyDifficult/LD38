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
    shooter = transform.root.GetComponentInChildren<Shoot>();
  }

  // Update is called once per frame
  void Update()
  {
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
