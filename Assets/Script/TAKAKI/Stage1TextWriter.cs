using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1TextWriter : MonoBehaviour
{
    public Stage1UIText uitext;

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
        uitext.DrawNameText("�i�ߊ�", "���߂č����e��`����"+ A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "���͂ȃr�[������u�q���O���C�v�ɂ��A�G�����s���̂����Ă���̂��{�~�b�V�����̖ړI�ł���B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "�u�q���O���C�v�𓖂Ă邽�߂ɂ́A���m�ȏƏ��ƃG�l���M�[�[�U�܂Ń��c�ɋC�Â���Ȃ��K�v������B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "���c�ɐڋ߂��Ă̏Ə��⍲�ƁA���ӂ����������Ă��炤���Ƃ��N�ɉۂ���ꂽ�C�����B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "�B��A������\�ɂ���̂��N�̓��悵�Ă���u�u���[�A�T���g�v�ł���B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "�՗�ȍU�����\�z����邪�A�����퓬�ƃo���A�t�B�[���h�𓋍ڂ��邻�̋@�̂ł���ΐ؂蔲���邱�Ƃ��\�Ȃ͂����B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "���c�̒��ӂ��Ђ����߁A��b�ł������퓬���p������B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("�i�ߊ�", "�ȏゾ�B�������F��B" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "�܂��Ȃ��G�����s���̂̍U�������ɓ˓����܂��B���ӂ��Ă�������" + A);
        yield return StartCoroutine("Skip");
        //yield return null;
    }
}
