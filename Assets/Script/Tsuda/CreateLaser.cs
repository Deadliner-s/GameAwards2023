using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // プレハブオブジェクト
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;
    public float Interval = 2.0f;
    public float Interval2 = 8.0f;

    private float timer = 0.0f;
    private float timer2 = 0.0f;
    private bool wait = false;

//    private GameObject targetObject; // 対象オブジェクト

    void Start()
    {        
        AtkPhaseFlg = PhaseObj.activeSelf;
    }

    void Update()
    {
        AtkPhaseFlg = PhaseObj.activeSelf;

        // 対象オブジェクトが存在しない場合は、処理を実行しない
        if (AtkPhaseFlg == false)
        {
            return;
        }
        else
        {

            if (!wait)
            {
                timer += Time.deltaTime;
                timer2 += Time.deltaTime;

                if (timer >= Interval)
                {
                    Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
                    timer = 0.0f; // タイマーをリセットする
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
}
