using UnityEngine;


public class Game : MonoBehaviour
{
    public static SoundManager SoundManager { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

