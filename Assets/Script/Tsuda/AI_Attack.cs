using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{
    public Canvas message; // CanvasオブジェクトをInspectorから指定する
    public AudioClip audioClip;
    AudioSource audioSource;

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
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        if (currentPhase == PhaseManager.Phase.Attack_Phase)
        {
            timer += Time.deltaTime;

            if (!AudioFlg)
            {
                audioSource.Play();
                AudioFlg = true;
            }

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
            AudioFlg = false;
        }
    }
}
