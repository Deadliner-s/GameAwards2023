using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighSpeedEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab; // �v���n�u�I�u�W�F�N�g
    [SerializeField]
    private GameObject PlayerPos; // �e�I�u�W�F�N�g

    [Tooltip("�G�t�F�N�g�̎�������")]
    [SerializeField]
    private float EffectDuration = 10.0f; // �e�I�u�W�F�N�g

    bool instantiateflag = true;

    private PhaseManager.Phase PhaseFlg; // �t�F�[�Y�t���O


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

        // �ΏۃI�u�W�F�N�g�����݂���ꍇ�́A���������s���Ȃ�
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
            // �n�C�X�s�[�h�t�F�[�Y���Ɉ�x��������
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
