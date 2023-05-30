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
    public float SpawnMissileRandom;
    private float RandomHalf;
    public float MissileDestroyTime;
    public float Accel;
    public float AccelStart;
    GameObject BossFlg;

    Vector3 move;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        BossFlg = GameObject.Find("BossManager");
        if (!BossFlg.GetComponent<MainBossHp>().BreakFlag)
        {
            move = new Vector3(moveX, moveY, 0.0f);
            time = 0.0f;
            Destroy(gameObject, DestroyTime);
            RandomHalf = Random.Range(-SpawnMissileRandom, SpawnMissileRandom);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!BossFlg.GetComponent<MainBossHp>().BreakFlag)
        {
            time += Time.timeScale;
            if (time >= SpawnMissileTime + RandomHalf)
            {
                time = 0.0f;
                GameObject obj = Instantiate(Missile, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.05f), Quaternion.Euler(0, 0, -90));
                // MissileObjをタグ検索
                GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
                // ミサイルオブジェクトの子にする
                obj.transform.parent = missileObj.transform;
                obj.GetComponent<ZakoMissile>().Speed = MissileSpeed;
                obj.GetComponent<ZakoMissile>().DestroyTime = MissileDestroyTime;
                obj.GetComponent<ZakoMissile>().Accel = Accel;
                obj.GetComponent<ZakoMissile>().AccelStart = AccelStart;
            }
            gameObject.transform.position += move * ZakoSpeed * Time.timeScale;
        }
        else
        {           
            Destroy(this, 0.0f);
        }
    }
}
