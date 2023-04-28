using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIText : MonoBehaviour
{
    // nameText:�����Ă���l�̖��O
    // talkText:�����Ă�����e��i���[�V����
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI talkText;

    public bool playing = false;
    public float textSpeed = 0.1f;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;

    void Start() { nextPhase = currntPhase; }

    // �N���b�N�Ŏ��̃y�[�W��\�������邽�߂̊֐�
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    // �i���[�V�����p�̃e�L�X�g�𐶐�����֐�
    public void DrawText(string text)
    {
        nameText.text = "";
        StartCoroutine("CoDrawText", text);
    }
    // �ʏ��b�p�̃e�L�X�g�𐶐�����֐�
    public void DrawNameText(string name, string text)
    {
        nameText.text = name;
        StartCoroutine("CoDrawText", text);
    }

    private void Update()
    {
        currntPhase = PhaseManager.instance.GetPhase();

        if (nextPhase != currntPhase)
        {
            nextPhase = currntPhase;
            if (currntPhase == PhaseManager.Phase.Normal_Phase)
            {


                //SeFlg = false;
            }
            else if (currntPhase == PhaseManager.Phase.Speed_Phase)
            {
                // �n�C�X�s�[�h�t�F�C�Y
                DrawNameText("AI", "�G��͂����ʂ̃~�T�C�����m�F�B���C���G���W���ɃG�l���M�[���[�U�B����s���ɐ�O���Ă��������B");

            }
            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // �A�^�b�N�t�F�C�Y
                DrawNameText("AI", "�G��͗�p��Ԃ��m�F�B�U�������ɃG�l���M�[���[�U�B�G��͂��U�����Ă��������B");

            }

        }
    }

    // �e�L�X�g���k���k���o�Ă��邽�߂̃R���[�`��
    IEnumerator CoDrawText(string text)
    {
        playing = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // �N���b�N�����ƈ�C�ɕ\��
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > text.Length) break;
            talkText.text = text.Substring(0, len);
        }
        talkText.text = text;
        yield return 0;
        playing = false;
    }
}