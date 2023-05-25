using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandooo : MonoBehaviour
{
    [Header("デバッグ用\n(ゲーム実行前に設定してください)")]
    [Header("デバッグモード")]
    [SerializeField] bool debugMode = false;
    [Header("ダメージを0にします")]
    [SerializeField] bool debugDamage = false;
    [Header("シーンをスキップします")]
    [SerializeField] bool debugScene = false;

    public static DebugCommandooo instance;

    // ダメージ設定のセット用
    public bool debugDamageSet { get; private set; }
    // シーン設定のセット用
    public bool debugSceneSet { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        // デバッグモードがOFFならデバッグモードにしない
        if (!debugMode)
        {
            // ダメージ設定のセット
            debugDamageSet = false;
            // シーン設定のセット
            debugSceneSet = false;

            return;
        }

        // ダメージ設定のセット
        debugDamageSet = debugDamage;
        // シーン設定のセット
        debugSceneSet = debugScene;
    }
}
