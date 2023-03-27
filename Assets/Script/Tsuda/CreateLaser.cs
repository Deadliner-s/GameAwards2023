using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // プレハブオブジェクト    

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;

    void Start()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();
    }

    void Update()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            if (Input.GetKeyDown(KeyCode.C)) // Cキーが押されたら
            {
                Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
            }
        }
    }
}

