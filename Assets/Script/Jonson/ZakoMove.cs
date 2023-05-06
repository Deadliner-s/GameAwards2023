    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoMove : MonoBehaviour
{
    public float moveY;
    public float moveX;
    public float ZakoSpeed = 0.01f;
    public float DestroyTime;

    public GameObject Missile;
    public float MissileSpeed;
    public float SpawnMissileTime;
    public float MissileDestroyTime;
    public float Accel;
    public float AccelStart;

    Vector3 move;
    int time;
    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(moveX, moveY, 0.0f);
        time = 0;
        Destroy(gameObject, DestroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        time += 1;
        if(time >= SpawnMissileTime)
        {
            time = 0;
            GameObject obj = Instantiate(Missile, transform.position, Quaternion.Euler(0,0,-90));
            obj.GetComponent<ZakoMissile>().Speed = MissileSpeed;
            obj.GetComponent<ZakoMissile>().DestroyTime = MissileDestroyTime;
            obj.GetComponent<ZakoMissile>().Accel = Accel;
            obj.GetComponent<ZakoMissile>().AccelStart = AccelStart;
        }
        gameObject.transform.position += move * ZakoSpeed;
    }
}
