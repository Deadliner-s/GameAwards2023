using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float PlayerDamage { get; private set; }
    public float EnemyDamage { get; private set; }

    // ダメージ名
    [Header("ダメージ名")]
    [Tooltip("プレイヤーへのダメージ")]
    [SerializeField] DamageManager.DAMAGE_NAME playerDamageName = DamageManager.DAMAGE_NAME.E_DAMAGE_MAX;
    [Tooltip("ボスへのダメージ")]
    [SerializeField] DamageManager.DAMAGE_NAME bossDamageName = DamageManager.DAMAGE_NAME.E_DAMAGE_MAX;

    // ダメージマネージャーオブジェクト
    private GameObject DamageAmount;

    private void Start()
    {
        // ダメージマネージャーを取得
        DamageAmount = GameObject.Find("DamageManager");
        //---- インスペクターで設定した与えるダメージを代入 ----
        // プレイヤーへのダメージ
        PlayerDamage = DamageAmount.GetComponent<DamageManager>().GetDamage(playerDamageName);
        // ボスへのダメージ
        EnemyDamage = DamageAmount.GetComponent<DamageManager>().GetDamage(bossDamageName);
    }
}
