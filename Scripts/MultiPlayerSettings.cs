using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerSettings : MonoBehaviour
{
    public static MultiPlayerSettings multiPlayerSettings;
    public bool delayStart; //true for delay
    public int maxPlayers;

    public int menuScene;
    public int multiPlayerScene;
    // Start is called before the first frame update
    private void Awake()
    {
        if(MultiPlayerSettings.multiPlayerSettings == null)
        {
            MultiPlayerSettings.multiPlayerSettings = this;
        }
        else
        {
            if(MultiPlayerSettings.multiPlayerSettings != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
