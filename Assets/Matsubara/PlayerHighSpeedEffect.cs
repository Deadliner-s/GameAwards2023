using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighSpeedEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab; // プレハブオブジェクト
    [SerializeField]
    private GameObject PlayerPos; // 親オブジェクト

    [Tooltip("エフェクトの持続時間")]
    [SerializeField]
    private float EffectDuration = 10.0f; // 親オブジェクト

    bool instantiateflag = true;

    private PhaseManager.Phase PhaseFlg; // フェーズフラグ


    // Start is called before the first frame update
    void Start()
    {
        PhaseFlg = PhaseManager.instance.GetPhase();
        instantiateflag = true;
    }

    // Update is called once per frame
    void Update()
    {
        PhaseFlg = PhaseManager.instance.GetPhase();

        // 対象オブジェクトが存在する場合は、処理を実行しない
        if (PhaseFlg == PhaseManager.Phase.Normal_Phase)
        {
            instantiateflag = true;
            return;
        }
        if (PhaseFlg == PhaseManager.Phase.Attack_Phase)
        {
            instantiateflag = true;
            return;
        }
        if (PhaseFlg == PhaseManager.Phase.Speed_Phase)
        {
            // ハイスピードフェーズ時に一度だけ生成
            if (instantiateflag == true)
            {
                var parent = PlayerPos.transform;
                GameObject obj = Instantiate(prefab, PlayerPos.transform.position, prefab.transform.rotation, parent);
                Destroy(obj, EffectDuration);

                instantiateflag = false;
                return;
            }
        }
    }

}
