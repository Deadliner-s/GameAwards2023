using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage12UIText : MonoBehaviour
{
    // nameText:�����Ă���l�̖��O
    // talkText:�����Ă�����e��i���[�V����
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI talkText;

    public bool playing = false;
    public float textSpeed = 0.1f;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;

    float AddTime = 0.0f;

    bool FlgA = true;
    bool FlgB = true;

    void Start()
    {
        nextPhase = currntPhase;
        StartCoroutine(A());
    }

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

    IEnumerator A()
    {
        while (true)
        {
            yield return null;
            currntPhase = PhaseManager.instance.GetPhase();
            if (nextPhase != currntPhase)
            {
                FlgA = true;
                FlgB = true;
                //AddTime = 0.0f;

                nextPhase = currntPhase;
            }
            if (currntPhase == PhaseManager.Phase.Normal_Phase)
            {


                //SeFlg = false;
            }
            else if (currntPhase == PhaseManager.Phase.Speed_Phase)
            {
                AddTime = 0.0f;
                // �n�C�X�s�[�h�t�F�C�Y
                if (FlgA)
                {
                    Window.SetActive(true);
                    Name.SetActive(true);
                    Text.SetActive(true);
                    DrawNameText("AI", "�U�������ւ̃G�l���M�[���J�b�g�B ���C���G���W���ɃG�l���M�[��]���B����s���ɐ�O���ĉ������B");
                    FlgA = false;
                }

                while (FlgB)
                {
                    AddTime += Time.deltaTime;
                    Debug.Log(AddTime);
                    yield return null;
                    if (AddTime >= 5.0f)
                    {
                        Name.SetActive(false);
                        Text.SetActive(false);
                        Window.SetActive(false);
                        FlgB = false;
                        break;
                    }
                }
            }

            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                AddTime = 0.0f;
                // �A�^�b�N�t�F�C�Y
                if (FlgA)
                {
                    Window.SetActive(true);
                    Name.SetActive(true);
                    Text.SetActive(true);
                    DrawNameText("AI", "�G��������A�����̔M���������m�F�B�O�a�U�����\������܂��B���ӂ��Ă��������B");
                    FlgA = false;
                }
                while (FlgB)
                {
                    AddTime += Time.deltaTime;
                    Debug.Log(AddTime);
                    yield return null;
                    if (AddTime >= 5.0f)
                    {
                        Name.SetActive(false);
                        Text.SetActive(false);
                        Window.SetActive(false);
                        FlgB = false;
                        break;
                    }
                }
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
            //if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > text.Length) break;
            talkText.text = text.Substring(0, len);
        }
        talkText.text = text;
        yield return 0;
        playing = false;
    }
}