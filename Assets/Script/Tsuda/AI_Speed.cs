using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Speed : MonoBehaviour
{
    public Canvas message; // CanvasオブジェクトをInspectorから指定する
    public float showtime = 7.0f;

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;

    private bool AtkPhaseFlg;
    private bool AudioFlg = false;
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

            if (!AudioFlg)
            {
                SoundManager.instance.PlayVOICE("AI_SPEED");
                AudioFlg = true;
            }

            if (timer <= showtime)
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
            AudioFlg = false;
        }
    }
}

