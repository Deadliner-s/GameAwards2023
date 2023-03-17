using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnMissile2 : MonoBehaviour
{
    public GameObject Missile;
    public float DestroyTime;
    public string Key;

    public float DelayTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            Invoke("InstantiateObject", DelayTime);
        }     
    }

    void InstantiateObject()
    {
        GameObject obj = Instantiate(Missile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(obj, DestroyTime);
    }
}
