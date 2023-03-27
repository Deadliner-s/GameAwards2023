using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Speed : MonoBehaviour
{
    public Canvas message; // CanvasオブジェクトをInspectorから指定する

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;

    private bool AtkPhaseFlg;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        message.enabled = false;
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();
    }

    // Update is called once per frame
    void Update()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            timer += Time.deltaTime;

            if (timer <= 5.0f)
            {
                message.enabled = true;
            }
            else
            {
                message.enabled = false;
            }
        }
        else
        {
            timer = 0.0f;
            message.enabled = false;
        }
    }
}

