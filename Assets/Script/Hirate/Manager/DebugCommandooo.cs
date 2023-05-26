using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandooo : MonoBehaviour
{
    [Header("デバッグ用\n(ゲーム実行前に設定してください)")]
    [Header("デバッグモード")]
    [SerializeField] bool debugMode = false;
    [Header("シーンをスキップします")]
    [SerializeField] bool debugScene = false;
    [Header("自爆スイッチを持たせますか？")]
    [SerializeField] bool debugExplosion = false;
    [Header("ダメージを0にします")]
    [SerializeField] bool debugDamage = false;
    [Header("ミサイルのデバッグの有無")]
    [SerializeField] bool debugMissile = false;

    public static DebugCommandooo instance;

    // シーン設定のセット用
    public bool debugSceneSet { get; private set; }
    // 自爆スイッチ設定のセット用
    public bool debugExplosionSet { get; private set; }
    // ダメージ設定のセット用
    public bool debugDamageSet { get; private set; }
    // ミサイル設定のセット用
    public bool debugMissileSet { get; private set; }

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
            // シーン設定のセット
            debugSceneSet = false;
            // 自爆スイッチ設定のセット
            debugExplosionSet = false;
            // ダメージ設定のセット
            debugDamageSet = false;
            // ミサイル設定のセット
            debugMissileSet = false;

            return;
        }

        // シーン設定のセット
        debugSceneSet = debugScene;
        // シーン設定のセット
        debugExplosionSet = debugExplosion;
        // ダメージ設定のセット
        debugDamageSet = debugDamage;
        // ミサイル設定のセット
        debugMissileSet = debugMissile;
    }
}
