using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    public bool Stage1;
    public bool Stage2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Stage1 = false;
        Stage2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReSetData()
    {
        Stage1 = false;
        Stage2 = false;
    }

    // ���j���[�̃R���e�j���[�Ŏg�p
    public int GetNowStage()
    {
        // ������
        int num = 0;

        // �N���A�t���O���m�F���Ď��̃X�e�[�W�I��
        if (Stage1 == false)
        {
            // �X�e�[�W1
            num = 0;
        }
        else if (Stage2 == false)
        {
            // �X�e�[�W2
            num = 1;
        }
        else
        {
            // �X�e�[�W3
            num = 2;
        }

        return num;
    }

    // �X�e�[�W�N���A����
    public void SetClearFlg(int num)
    {
        switch (num)
        {
            case (1):
                Stage1 = true;
                break;
            case (2):
                Stage2 = true;
                break;
            default:
                break;
        }
    }
}
