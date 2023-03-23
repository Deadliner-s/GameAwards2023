using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile : MonoBehaviour
{  
    public GameObject Missile;
    public float DestroyTime;
    public string Key;
    public float Interval = 2.0f;   //出す時間
    public float Interval2 = 8.0f;  //出す間隔

    private float timer = 0.0f;
    private float timer2 = 0.0f;

    private bool wait = false;

    //フェイズ切り替え用
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;

    // Start is called before the first frame update
    void Start()
    {
        // フェイズ取得
        AtkPhaseFlg = PhaseObj.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        // フェイズ確認
        AtkPhaseFlg = PhaseObj.activeSelf;

        if (Input.GetKeyDown(Key))
        {
            GameObject obj = Instantiate(Missile, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(obj, DestroyTime);
        }

        if (!wait)
        {
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;

            if (timer >= Interval)
            {
                // フェイズの確認
                if (AtkPhaseFlg == true)
                {
                    GameObject obj = Instantiate(Missile, new Vector3(0, 0, 0), Quaternion.identity);
                    Destroy(obj, DestroyTime);
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