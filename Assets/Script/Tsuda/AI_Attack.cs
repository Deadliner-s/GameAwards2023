using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{
    public Canvas message; // Canvas�I�u�W�F�N�g��Inspector����w�肷��

    // ���݃t�F�C�Y
    private PhaseManager.Phase currentPhase;

    private bool AtkPhaseFlg;
    private bool AudioFlg = false;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {        
        message.enabled = false;
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();
    }

    // Update is called once per frame
    void Update()
    {
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();

        if (currentPhase == PhaseManager.Phase.Attack_Phase)
        {
            timer += Time.deltaTime;

            if (!AudioFlg)
            {
                //SoundManager.instance.PlayVOICE("AI_ATTACK");
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
