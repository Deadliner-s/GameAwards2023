using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile2 : MonoBehaviour
{
    public GameObject Missile;
    public float DestroyTime;
    public float Interval = 2.0f;   //出す時間
    public float Interval2 = 8.0f;  //出す間隔

    private float timer = 0.0f;
    private float timer2 = 0.0f;

    private bool wait = false;

    public string Key;              //ボタンをしたら出す　時間無視

    GameObject obj;

    public float DelayTime = 0.0f;

    Vector3 ToPos;              //発射先

    public GameObject SpawnPos;

    //フェイズ切り替え用
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Player"))//プレイヤーは生きている（存在する）
            ToPos = GameObject.Find("Player").transform.position;

        // フェイズ取得
        AtkPhaseFlg = PhaseObj.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player"))//プレイヤーは生きている（存在する）
        {
            if (Input.GetKeyDown(Key))
            {
                Invoke("InstantiateObject", DelayTime);
                ToPos = GameObject.Find("Player").transform.position;
            }

            if (!wait)
            {
                timer += Time.deltaTime;
                timer2 += Time.deltaTime;

                if (timer >= Interval)
                {
                    // フェイズ確認
                    AtkPhaseFlg = PhaseObj.activeSelf;

                    // フェイズの確認
                    if (AtkPhaseFlg == false)
                    {
                        Invoke("InstantiateObject", DelayTime);
                        timer = 0.0f; // タイマーをリセットする
                    }
                }
                if (timer2 >= Interval2)
                {
                    wait = true;
                    timer2 = 0.0f;
                }
            }
            if (wait)
            {
                timer2 += Time.deltaTime;
                if (timer2 >= Interval2)
                {
                    wait = false;
                    timer2 = 0.0f;
                }
            }

        }
           
    }

    void InstantiateObject()
    {
        obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
        Destroy(obj, DestroyTime);
    }
}