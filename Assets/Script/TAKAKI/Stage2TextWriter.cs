using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2TextWriter : MonoBehaviour
{
    public Stage2UIText uitext;

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
        string A = "�@�@�@�@�@�@�@�@�@�@";

        uitext.DrawNameText("", " ");
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "�q���O���C�A�G�l���M�[�[�U�����܂�10�b" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "9" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "8" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "7" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "6" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "5" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "4" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "3" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "2" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "1" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "�q���O���C�A���āI" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "�ڕW�ւ̒��e���m�F�B�ڕW�𕢂��o���A�t�B�[���h�A���܂����݂ł��B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "���Ŏd���߂�B�q���O���C�A�ď[�U���J�n����B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "�����A�q���̈ʒu�����c�ɕ⑫���ꂽ�\��������B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "����ɐڋ߂��A���c�̒��ӂ������t���Ă���I" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "�G�����s���̂ɐڋ߂��܂��B����Ȃ�U���ɒ��ӂ��Ă��������B" + A);
        yield return StartCoroutine("Skip");

        //yield return null;
    }
}
