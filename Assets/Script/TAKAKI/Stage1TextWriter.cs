using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1TextWriter : MonoBehaviour
{
    public Stage1UIText uitext;

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

        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("0-1");
        uitext.DrawNameText("�� �i�ߊ� ��", "���߂č����e��`����");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�� �i�ߊ� ��", "���͂ȃr�[������u�q���O���C�v�ɂ��A�G�����s���̂�\n���Ă���̂��{�~�b�V�����̖ړI�ł���B");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�� �i�ߊ� ��", "�u�q���O���C�v�𓖂Ă邽�߂ɂ́A���m�ȏƏ��ƃG�l���M�[�[�U�܂Ń��c�ɋC�Â���Ȃ��K�v������B");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�� �i�ߊ� ��", "���c�ɐڋ߂��Ă̏Ə��⍲�ƁA���ӂ����������Ă��炤���Ƃ�\n�N�ɉۂ���ꂽ�C�����B");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�� �i�ߊ� ��", "�B��A������\�ɂ���̂��N�̓��悵�Ă���\n�u�u���[�A�T���g�v�ł���B");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�� �i�ߊ� ��", "�՗�ȍU�����\�z����邪�A�����퓬�ƃo���A�t�B�[���h�𓋍ڂ���\n���̋@�̂ł���ΐ؂蔲���邱�Ƃ��\�Ȃ͂����B");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�� �i�ߊ� ��", "���c�̒��ӂ��Ђ����߁A��b�ł������퓬���p������B");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�� �i�ߊ� ��", "�ȏゾ�B�������F��B");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�� AI ��", "�܂��Ȃ��G�����s���̂̍U�������ɓ˓����܂��B���ӂ��Ă�������");
        yield return new WaitForSeconds(3.0f);

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);
        //yield return null;
    }
}
