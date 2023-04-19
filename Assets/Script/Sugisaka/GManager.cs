using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    public bool Stage1;
    public bool Stage2;
    public bool Stage3;

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

    // Start is called before the first frame update
    void Start()
    {
        Stage1 = false;
        Stage2 = false;
        Stage3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
