using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighSpeedEffect : MonoBehaviour
{
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;
    [SerializeField]
    public GameObject prefab; // プレハブオブジェクト
    [SerializeField]
    public GameObject PlayerPos; // 親オブジェクト

    bool instantiateflag = true;

    // Start is called before the first frame update
    void Start()
    {
        AtkPhaseFlg = PhaseObj.activeSelf;
        instantiateflag = true;
    }

    // Update is called once per frame
    void Update()
    {
        AtkPhaseFlg = PhaseObj.activeSelf;

        // 対象オブジェクトが存在する場合は、処理を実行しない
        if (AtkPhaseFlg != false)
        {
            instantiateflag = true;
            return;
        }
        else
        {
            // ハイスピードフェーズ時に一度だけ生成
            if (instantiateflag == true) {
                var parent = PlayerPos.transform;
                GameObject obj = Instantiate(prefab, PlayerPos.transform.position,prefab.transform.rotation,parent);
                Destroy(obj, 10.0f);
                instantiateflag = false;
            }
        }
    }

}
