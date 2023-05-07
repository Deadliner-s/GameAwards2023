using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    [Header("Bossの回転速度")]
    [Tooltip("通常")]
    [SerializeField] private float rotationSpeedNormal = 0.1f;
    [Tooltip("アタック")]
    [SerializeField] private float rotationSpeedAttack = 0.1f;
    [Tooltip("ハイスピード")]
    [SerializeField] private float rotationSpeedHighSpeed = 0.1f;

    private float rotationSpeed; // 回転速度
    private PhaseManager.Phase PhaseFlg; // フェーズフラグ

    // Start is called before the first frame update
    void Start()
    {
        // フェーズ取得用
        try
        {
            PhaseFlg = PhaseManager.instance.GetPhase();
        }
        catch
        {

        }

        // 通常フェーズを代入
        PhaseFlg = PhaseManager.Phase.Normal_Phase;
    }

    // Update is called once per frame
    void Update()
    {
        // フェーズ取得用
        try
        {
            PhaseFlg = PhaseManager.instance.GetPhase();
        }
        catch
        {

        }

        // フェーズによって切り替え
        // 通常
        if (PhaseFlg == PhaseManager.Phase.Normal_Phase) {
            rotationSpeed = rotationSpeedNormal;
        }
        // アタック
        if (PhaseFlg == PhaseManager.Phase.Attack_Phase) {
            rotationSpeed = rotationSpeedAttack;
        }
        // ハイスピード
        if (PhaseFlg == PhaseManager.Phase.Speed_Phase) {
            rotationSpeed = rotationSpeedHighSpeed;
        }
        // 動いているように見せる
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
