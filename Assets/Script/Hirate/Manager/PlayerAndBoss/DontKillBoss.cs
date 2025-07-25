using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontKillBoss : MonoBehaviour
{
    public static DontKillBoss instance;

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
