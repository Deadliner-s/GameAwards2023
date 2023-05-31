using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3TextWriter : MonoBehaviour
{
    public Stage3UIText uitext;

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;

    private float Max_Height = 2.756843f;

    // Start is called before the first frame update
    void Start()
    {
        //Window.SetActive(false);
        //Name.SetActive(false);
        //Text.SetActive(false);

        StartCoroutine("Cotest");
    }

    // �N���b�N�҂��̃R���[�`��
    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        //while (!uitext.IsClicked()) yield return 0;
    }

    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        uitext.DrawNameText("�� �i�ߊ� ��", "���x�������B�q���O���C�A���āI");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("2-12");
        uitext.DrawNameText("�� AI ��", "�ڕW�֒��e�B�G�����s���̂̃o���A�t�B�[���h�j����m�F���܂����B");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("2-13");
        uitext.DrawNameText("�� �i�ߊ� ��", "����ł����Ăł��񂩁B��������");
        yield return new WaitForSeconds(4.0f);

        SoundManager.instance.PlayVOICE("2-14");
        uitext.DrawNameText("�� AI ��", "�G�����s���́A�G�l���M�[�̋}���ȏ㏸���m�F�B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-15");
        uitext.DrawNameText("�� �i�ߊ� ��", "���H");
        yield return new WaitForSeconds(2.0f);

        SoundManager.instance.PlayVOICE("2-16");
        uitext.DrawNameText("�� AI ��", "�ڕW�\���c�c�U���ڕW�́A�q���O���C�ł��B");
        yield return new WaitForSeconds(12.0f);

        SoundManager.instance.PlayVOICE("2-17");
        uitext.DrawNameText("�� �i�ߊ� ��", "�q���O���C�����ꂽ�B�����A���I���͔F�߂��Ȃ��B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-18");
        uitext.DrawNameText("�� �i�ߊ� ��", "���c�̃o���A�t�B�[���h���j��ł������Ȃ�A\n�u���[�A�T���g�̃~�T�C���ɂ��j�󂪉\�Ȃ͂����B");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("2-19");
        uitext.DrawNameText("�� �i�ߊ� ��", "����ɐڋ߂��A���c�𒼐ڍU�����Ă���B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-20");
        uitext.DrawNameText("�� �i�ߊ� ��", "�N�ɐl�ނ̖��^������B�@�c�c���񂾂��B");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("2-21");
        uitext.DrawNameText("�� AI ��", "�G�����s���̂ɍŐڋ߂��܂��B\n����Ȃ�U���ɒ��ӂ��Ă��������B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-22");
        uitext.DrawNameText("�� AI ��", "�\��������퐬���m���A2���B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-tuika");
        uitext.DrawNameText("�� AI ��", "�c�c�c�ł����A");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("2-23");
        uitext.DrawNameText("�� AI ��", "�A�i�^�ƃ��^�V�Ȃ�A�ł��܂��B\n�����ċA��܂��傤�B");
        yield return new WaitForSeconds(6.0f);

        StartCoroutine("WindowScaleDown");

        // SceneMoveManager���^�O����
        GameObject obj = GameObject.FindGameObjectWithTag("NowEventSceneSet");
        // �V�[���̊J�n
        obj.GetComponent<SceneEventMove>().bTextEnd = true;
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
