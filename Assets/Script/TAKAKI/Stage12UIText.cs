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
    bool FlgFinish = false;

    private float Max_Height = 1.837893f;
    float MessageWindowActiveTime = 8.0f;
    float WarningActiveTime = 13.0f;
    int Attacknum = 0;


    void Start()
    {
        AddTime = 0.0f;
        FlgFinish = false;
        Attacknum = 0;

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);

        nextPhase = currntPhase;
        //StartCoroutine(A());
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
        if (currntPhase == PhaseManager.Phase.Normal_Phase && FlgFinish == false)
        {

        }
        // �A�^�b�N�t�F�C�Y
        else if (currntPhase == PhaseManager.Phase.Attack_Phase && FlgFinish == false && Attacknum < 3)
        {
            if (FlgA)
            {
                // �E�B���h�E�̊J�n
                StartCoroutine("AttackMessageUI");
                FlgA = false;
                Attacknum++;
            }
        }
        else if (currntPhase == PhaseManager.Phase.Speed_Phase && FlgFinish == false)
        {

        }

        if (AddTime >= 150.0f)
        {
            if (FlgB)
            {
                // �J�E���g�_�E���̊J�n
                StartCoroutine("CountDownUI");
                FlgFinish = true;
                FlgB = false;
            }
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

    //IEnumerator A()
    //{
    //    while (true)
    //    {
    //        yield return null;
            
    //    }
    //}

    // �Q�[������AI�Z���t�p
    IEnumerator AttackMessageUI()
    {
        // �E�B���h�E�\��
        StartCoroutine("WindowScaleUp");
        // �{�C�X�Đ�
        SoundManager.instance.PlayVOICE("EX-1");
        // �e�L�X�g�ݒ�
        DrawNameText("�� AI ��", "�G��������A�����̔M���������m�F�B\n�O�a�U�����\������܂��B���ӂ��Ă��������B");
        // �E�B���h�E�\������
        yield return new WaitForSeconds(MessageWindowActiveTime);
        // �E�B���h�E��\��
        StartCoroutine("WindowScaleDown");

        // �A�^�b�N�t�F�C�Y�̊ԑҋ@
        yield return new WaitForSeconds(WarningActiveTime - MessageWindowActiveTime);

        StartCoroutine("WindowScaleUp");
        // �{�C�X�Đ�
        SoundManager.instance.PlayVOICE("4-2");
        // �e�L�X�g�ݒ�
        DrawNameText("�� AI ��", "�U�������ւ̃G�l���M�[���J�b�g�B ���C���G���W����\n�G�l���M�[��]���B����s���ɐ�O���ĉ������B");
        // �E�B���h�E�\������
        yield return new WaitForSeconds(MessageWindowActiveTime);
        // �E�B���h�E��\��
        StartCoroutine("WindowScaleDown");
    }

    IEnumerator CountDownUI()
    {
        // �E�B���h�E�\��
        StartCoroutine("WindowScaleUp");

        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-1");
        DrawNameText("�� AI ��", "�q���O���C�A�G�l���M�[�[�U�����܂�10�b");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("1-2");
        DrawNameText("�� AI ��", " 9");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-3");
        DrawNameText("�� AI ��", " 8");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-4");
        DrawNameText("�� AI ��", " 7");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-5");
        DrawNameText("�� AI ��", " 6");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-6");
        DrawNameText("�� AI ��", " 5");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-7");
        DrawNameText("�� AI ��", " 4");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-8");
        DrawNameText("�� AI ��", " 3");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-9");
        DrawNameText("�� AI ��", " 2");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-10");
        DrawNameText("�� AI ��", " 1");
        yield return new WaitForSeconds(1.0f);

        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1)
        {
            SoundManager.instance.PlayVOICE("1-11");
            DrawNameText("�� �i�ߊ� ��", "�q���O���C�A���āI");
        }
        else if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2)
        {
            SoundManager.instance.PlayVOICE("2-11");
            DrawNameText("�� �i�ߊ� ��", "���x�������B�q���O���C�A���āI");
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
    // �E�B���h�E�̊g��
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
    // �E�B���h�E�̏k��
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