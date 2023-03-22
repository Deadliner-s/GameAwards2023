using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{
    public Canvas message; // CanvasオブジェクトをInspectorから指定する
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;

    private bool AtkPhaseFlg;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        message.enabled = false;
        AtkPhaseFlg = PhaseObj.activeSelf;        
    }

    // Update is called once per frame
    void Update()
    {
        AtkPhaseFlg = PhaseObj.activeSelf;

        if (AtkPhaseFlg == true)
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
