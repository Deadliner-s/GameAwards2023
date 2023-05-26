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

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;

    float AddTime = 0.0f;

    bool FlgA = true;
    bool FlgB = true;
    
    private float Max_Height = 1.837893f;

    void Start()
    {
        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);

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
                    StartCoroutine("WindowScaleUp");
                    SoundManager.instance.PlayVOICE("4-1");
                    DrawNameText("�� AI ��", "�G��������A�����̔M���������m�F�B\n�O�a�U�����\������܂��B");
                    yield return new WaitForSeconds(6.5f);
                    SoundManager.instance.PlayVOICE("4-2");
                    DrawNameText("�� AI ��", "�U�������ւ̃G�l���M�[���J�b�g�B���C���G���W���ɃG�l���M�[��]���B����s���ɐ�O���ĉ������B");
                    yield return new WaitForSeconds(3.0f);
                    FlgA = false;
                }

                while (FlgB)
                {
                    AddTime += Time.deltaTime;
                    yield return null;
                    if (AddTime >= 5.0f)
                    {
                        StartCoroutine("WindowScaleDown");
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
                    StartCoroutine("WindowScaleUp");
                    SoundManager.instance.PlayVOICE("4-3");
                    DrawNameText("�� AI ��", "�G�A���b���x�㏸�B��p��Ԃւ̈ڍs���m�F�B");
                    yield return new WaitForSeconds(5.0f);
                    SoundManager.instance.PlayVOICE("4-4");
                    DrawNameText("�� AI ��", "�U�������ɃG�l���M�[��ڑ��B\n�I�o������p���u���U�����ĉ������B");
                    yield return new WaitForSeconds(1.0f);
                    FlgA = false;
                }
                while (FlgB)
                {
                    AddTime += Time.deltaTime;
                    yield return null;
                    if (AddTime >= 5.0f)
                    {
                        StartCoroutine("WindowScaleDown");
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
    IEnumerator WindowScaleUp()
    {
        Window.SetActive(true);
        Window.transform.localScale = new Vector3(
            Window.transform.localScale.x,
            0.0f,
            Window.transform.localScale.z
            );
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y += Max_Height * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }
        Name.SetActive(true);
        Text.SetActive(true);
    }
    IEnumerator WindowScaleDown()
    {
        Name.SetActive(false);
        Text.SetActive(false);
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y -= Max_Height * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }
        Window.SetActive(false);
    }
}