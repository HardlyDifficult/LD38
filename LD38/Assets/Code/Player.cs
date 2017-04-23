using UnityEngine;

public class Player : MonoBehaviour
{
    public LifeLine LifeLineComponent;

    public float Health
    {
        get { return LifeLineComponent.life; }
    }

    public string PlayerName = "Jane doe";

    public void Awake()
    {
        LifeLineComponent = GetComponent<LifeLine>();
    }

}

