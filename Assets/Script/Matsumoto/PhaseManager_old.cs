using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager_old : MonoBehaviour
{
    // フェイズ切り替え用
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;                   // フェイズ取得用
    private float time = 0.0f;                  // 秒数カウント用
    private bool boolValue = false;             // フェイズ切り替え用

    // フェイズが変わる秒数
    [Header("フェイズが変わる秒数")]
    public float PhaseChangeTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 最初のフェイズ取得
        AtkPhaseFlg = PhaseObj.activeSelf;
        // タイマー初期化
        time = 0.0f;

        // 最初のフェイズによって初期化を変える
        if (AtkPhaseFlg == false)
        {
            boolValue = false;
        }
        else
        {
            boolValue = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= PhaseChangeTime)
        {
            boolValue = !boolValue;
            PhaseObj.SetActive(boolValue);
            time = 0.0f;
        }
        
    }
}
