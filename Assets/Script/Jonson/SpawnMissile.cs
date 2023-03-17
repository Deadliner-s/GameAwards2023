using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnMissile : MonoBehaviour
{  
    public GameObject Missile;
    public float DestroyTime;
    public string Key;

    //public float FramePerMissle;
    //int FrameTime;

    // Start is called before the first frame update
    void Start()
    {
        //FrameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //FrameTime++;
        //if (FrameTime >= FramePerMissle)
        //{
        //    FrameTime = 0;
        //    GameObject obj = Instantiate(Missile, new Vector3(0, 0, 0), Quaternion.identity);
        //    Destroy(obj, DestroyTime);
        //}
        if(Input.GetKeyDown(Key))
        {
            GameObject obj = Instantiate(Missile, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(obj, DestroyTime);
        }
    }
}
