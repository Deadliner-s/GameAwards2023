using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public enum DAMAGE_NAME
    {
        // ボスへのダメージ
        E_PLAYER_ATTACK_DAMAGE = 0,

        // ミサイル
        E_BOSS_MISSLE_CLUSTER,       // クラスターミサイル
        E_BOSS_MISSLE_CLUSTER_SMALL, // クラスターミサイルの中身
        E_BOSS_MISSLE_CONTENA,       // コンテナミサイル
        E_BOSS_MISSLE_CONTENA_SMALL, // コンテナミサイルの中身
        E_BOSS_MISSLE_HOMING,        // 誘導ミサイル
        E_BOSS_MISSLE_SPEED,         // 高速ミサイル
        E_BOSS_MISSLE_ZAKO,          // 高速ミサイル

        // レーザー
        E_BOSS_LASER,

        // ダメージの名前の最大
        E_DAMAGE_MAX
    }

    // ダメージの変数
    [Header("ボスへのダメージ設定")]
    [SerializeField] float PlayerAttackDamage = 100.0f;
    [Header("プレイヤーへのダメージ設定")]
    [Tooltip("クラスターミサイル")]
    [SerializeField] float BossMissleCluster = 5.0f;
    [Tooltip("クラスターミサイルの中身")]
    [SerializeField] float BossMissleClusterSmall = 5.0f;
    [Tooltip("コンテナミサイル")]
    [SerializeField] float BossMissleContena = 5.0f;
    [Tooltip("コンテナミサイルの中身")]
    [SerializeField] float BossMissleContenaSmall = 5.0f;
    [Tooltip("誘導ミサイル")]
    [SerializeField] float BossMissleHoming = 5.0f;
    [Tooltip("高速ミサイル")]
    [SerializeField] float BossMissleSpeed = 5.0f;
    [Tooltip("雑魚のミサイル")]
    [SerializeField] float BossMissleZako = 5.0f;
    [Tooltip("レーザー")]
    [SerializeField] float BossLaser = 5.0f;

    // デバッグ用(ダメージを0にします)
    private bool debug = false;

    private void Start()
    {
        // デバッグ用の設定
        debug = DebugCommandooo.instance.debugDamageSet;
    }

    // ダメージ量取得用の関数
    public float GetDamage(DAMAGE_NAME damage_name)
    {
        // 返り値用の変数
        float work = 0.0f;

        // 返り値でダメージを返すための分岐
        switch (damage_name)
        {
            // クラスターミサイル
            case DAMAGE_NAME.E_BOSS_MISSLE_CLUSTER:
                work = BossMissleCluster;
                break;
            // クラスターミサイルの中身
            case DAMAGE_NAME.E_BOSS_MISSLE_CLUSTER_SMALL:
                work = BossMissleClusterSmall;
                break;
            // コンテナミサイル
            case DAMAGE_NAME.E_BOSS_MISSLE_CONTENA:
                work = BossMissleContena;
                break;
            // コンテナミサイルの中身
            case DAMAGE_NAME.E_BOSS_MISSLE_CONTENA_SMALL:
                work = BossMissleContenaSmall;
                break;
            // 誘導ミサイル
            case DAMAGE_NAME.E_BOSS_MISSLE_HOMING:
                work = BossMissleHoming;
                break;
            // 高速ミサイル
            case DAMAGE_NAME.E_BOSS_MISSLE_SPEED:
                work = BossMissleSpeed;
                break;
            // 雑魚のミサイル
            case DAMAGE_NAME.E_BOSS_MISSLE_ZAKO:
                work = BossMissleZako;
                break;
            // レーザー
            case DAMAGE_NAME.E_BOSS_LASER:
                work = BossLaser;
                break;
            // デバッグ用
            case DAMAGE_NAME.E_DAMAGE_MAX:
                work = 0.0f;
                break;
        }

        // デバッグ用
        if (debug)
        {
            // ダメージ量を0にする
            work = 0.0f;
        }

        // プレイヤーからのダメージはそのままにする
        if (damage_name == DAMAGE_NAME.E_PLAYER_ATTACK_DAMAGE)
        {
            work = PlayerAttackDamage;
        }

        // 返り値でダメージ量を返す
        return work;
    }
}
