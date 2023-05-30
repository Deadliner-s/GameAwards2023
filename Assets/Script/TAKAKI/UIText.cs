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
    
    private float Max_Height = 1.837893f;
    float WarningActiveTime = 33.0f;

    void Start()
    {
        AddTime = 0.0f;

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);

        nextPhase = currntPhase;
    }

    void Update()
    {
        AddTime += Time.deltaTime;

        // ���݂̃t�F�C�Y�m�F
        currntPhase = PhaseManager.instance.GetPhase();
        // �t�F�C�Y�ύX��
        if (nextPhase != currntPhase)
        {
            // �t���O���Z�b�g
            FlgA = true;
            nextPhase = currntPhase;
        }
        // �m�[�}���t�F�C�Y
        if (currntPhase == PhaseManager.Phase.Normal_Phase)
        {

        }
        // �A�^�b�N�t�F�C�Y
        else if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            if (FlgA)
            {
                // �E�B���h�E�̊J�n
                StartCoroutine("AttackMessageUI");
                FlgA = false;
            }
        }
        else if (currntPhase == PhaseManager.Phase.Speed_Phase)
        {

        }
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
    
    // �Q�[������AI�Z���t�p
    IEnumerator AttackMessageUI()
    {
        // �E�B���h�E�\��
        StartCoroutine("WindowScaleUp");
        // �{�C�X�Đ�,�e�L�X�g�ݒ�,�\��
        SoundManager.instance.PlayVOICE("4-3");
        DrawNameText("�� AI ��", "�G�A���b���x�㏸�B��p��Ԃւ̈ڍs���m�F�B");
        yield return new WaitForSeconds(5.0f);
        SoundManager.instance.PlayVOICE("4-4");
        DrawNameText("�� AI ��", "�U�������ɃG�l���M�[��ڑ��B\n�I�o������p���u���U�����ĉ������B");
        yield return new WaitForSeconds(6.0f);
        // �E�B���h�E��\��
        StartCoroutine("WindowScaleDown");

        // �A�^�b�N�t�F�C�Y�̊ԑҋ@
        yield return new WaitForSeconds(WarningActiveTime - 5.0f - 6.0f);

        StartCoroutine("WindowScaleUp");
        // �{�C�X�Đ�,�e�L�X�g�ݒ�,�\��
        SoundManager.instance.PlayVOICE("4-1");
        DrawNameText("�� AI ��", "�G��������A�����̔M���������m�F�B\n�O�a�U�����\������܂��B");
        yield return new WaitForSeconds(6.0f);
        SoundManager.instance.PlayVOICE("4-2");
        DrawNameText("�� AI ��", "�U�������ւ̃G�l���M�[���J�b�g�B���C���G���W����\n�G�l���M�[��]���B����s���ɐ�O���ĉ������B");
        yield return new WaitForSeconds(8.0f);
        // �E�B���h�E��\��
        StartCoroutine("WindowScaleDown");
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
        DrawNameText("", "");
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