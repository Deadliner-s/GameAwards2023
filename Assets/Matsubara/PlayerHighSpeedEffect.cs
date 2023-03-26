using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighSpeedEffect : MonoBehaviour
{
    [Header("�t�F�C�Y�m�F�p�I�u�W�F�N�g")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;
    [SerializeField]
    public GameObject prefab; // �v���n�u�I�u�W�F�N�g
    [SerializeField]
    public GameObject PlayerPos; // �e�I�u�W�F�N�g

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

        // �ΏۃI�u�W�F�N�g�����݂���ꍇ�́A���������s���Ȃ�
        if (AtkPhaseFlg != false)
        {
            instantiateflag = true;
            return;
        }
        else
        {
            // �n�C�X�s�[�h�t�F�[�Y���Ɉ�x��������
            if (instantiateflag == true) {
                var parent = PlayerPos.transform;
                GameObject obj = Instantiate(prefab, PlayerPos.transform.position,prefab.transform.rotation,parent);
                Destroy(obj, 10.0f);
                instantiateflag = false;
            }
        }
    }

}
