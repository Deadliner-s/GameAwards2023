using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontKillPlayer : MonoBehaviour
{
    public static DontKillPlayer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
