using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3TextWriter : MonoBehaviour
{
    public Stage3UIText uitext;

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;

    // Start is called before the first frame update
    void Start()
    {
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
        Window.SetActive(true);
        Name.SetActive(true);
        Text.SetActive(true);

        string A = "�@�@�@�@�@�@�@�@�@�@";

        uitext.DrawNameText("", " ");
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "�q���O���C�A�G�l���M�[�[�U�����܂�10�b" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "9" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "8" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "7" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "6" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "5" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "4" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "3" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "2" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "1" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("�i�ߊ�", "���x�������B�q���O���C�A���āI" + A);
        yield return new WaitForSeconds(2.0f);

        uitext.DrawNameText("AI", "�ڕW�֒��e�B�G�����s���̂̃o���A�t�B�[���h�j����m�F���܂����B" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�i�ߊ�", "����ł����Ăł��񂩁B��������" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "�G�����s���́A�G�l���M�[�̋}���ȏ㏸�����o" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�i�ߊ�", "���H" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "�\�z�U���ڕW�A�q���O���C�ł��B" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�i�ߊ�", "�q���O���C�����ꂽ�B�����A���I���͔F�߂��Ȃ��B" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�i�ߊ�", "���c�̃o���A�t�B�[���h���j��ł������Ȃ�A�u���[�A�T���g��\n�~�T�C���ɂ��j�󂪉\�Ȃ͂����B" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�i�ߊ�", "����ɐڋ߂��A���c�𒼐ڍU�����Ă���" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�i�ߊ�", "�N�ɐl�ނ̖��^������B�@�c�c���񂾂��B" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "�G�����s���̂ɍŐڋ߂��܂��B����Ȃ�U�����\�z����܂��B" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "�\��������퐬���m���A2���B" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "�A�i�^�ƃ��^�V�Ȃ�A�ł��܂��B�����ċA��܂��傤�B" + A);
        yield return new WaitForSeconds(3.0f);

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);
        //yield return null;
    }
}
