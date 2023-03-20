using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile2 : MonoBehaviour
{
    public GameObject Missile;
    public float DestroyTime;
    public string Key;
    GameObject obj;

    public float DelayTime = 3.0f;

    Vector3 ToPos;              //î≠éÀêÊ
  
    // Start is called before the first frame update
    void Start()
    {
        ToPos = GameObject.Find("Player").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            Invoke("InstantiateObject", DelayTime);
            ToPos = GameObject.Find("Player").transform.position;
        }

    }

    void InstantiateObject()
    {
        obj = Instantiate(Missile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(obj, DestroyTime);
    }
}
