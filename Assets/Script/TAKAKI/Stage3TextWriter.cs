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

        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-1");
        uitext.DrawNameText("�� AI ��", "�q���O���C�A�G�l���M�[�[�U�����܂�10�b");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("1-2");
        uitext.DrawNameText("�� AI ��", " 9");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-3");
        uitext.DrawNameText("�� AI ��", " 8");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-4");
        uitext.DrawNameText("�� AI ��", " 7");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-5");
        uitext.DrawNameText("�� AI ��", " 6");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-6");
        uitext.DrawNameText("�� AI ��", " 5");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-7");
        uitext.DrawNameText("�� AI ��", " 4");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-8");
        uitext.DrawNameText("�� AI ��", " 3");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-9");
        uitext.DrawNameText("�� AI ��", " 2");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-10");
        uitext.DrawNameText("�� AI ��", " 1");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("2-11");
        uitext.DrawNameText("�� �i�ߊ� ��", "���x�������B�q���O���C�A���āI");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("2-12");
        uitext.DrawNameText("�� AI ��", "�ڕW�֒��e�B�G�����s���̂�\n�o���A�t�B�[���h�j����m�F���܂����B");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("2-13");
        uitext.DrawNameText("�� �i�ߊ� ��", "����ł����Ăł��񂩁B��������");
        yield return new WaitForSeconds(4.0f);

        SoundManager.instance.PlayVOICE("2-14");
        uitext.DrawNameText("�� AI ��", "�G�����s���́A�G�l���M�[�̋}���ȏ㏸���m�F");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-15");
        uitext.DrawNameText("�� �i�ߊ� ��", "���H");
        yield return new WaitForSeconds(2.0f);

        SoundManager.instance.PlayVOICE("2-16");
        uitext.DrawNameText("�� AI ��", "�ڕW�\���c�c�U���ڕW�́A�q���O���C�ł��B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-17");
        uitext.DrawNameText("�� �i�ߊ� ��", "�q���O���C�����ꂽ�B�����A���I���͔F�߂��Ȃ��B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-18");
        uitext.DrawNameText("�� �i�ߊ� ��", "���c�̃o���A�t�B�[���h���j��ł������Ȃ�A\n�u���[�A�T���g�̃~�T�C���ɂ��j�󂪉\�Ȃ͂����B");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("2-19");
        uitext.DrawNameText("�� �i�ߊ� ��", "����ɐڋ߂��A���c�𒼐ڍU�����Ă���");
        yield return new WaitForSeconds(4.0f);

        SoundManager.instance.PlayVOICE("2-20");
        uitext.DrawNameText("�� �i�ߊ� ��", "�N�ɐl�ނ̖��^������B�@�c�c���񂾂��B");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("2-21");
        uitext.DrawNameText("�� AI ��", "�G�����s���̂ɍŐڋ߂��܂��B\n����Ȃ�U���ɒ��ӂ��Ă��������B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-22");
        uitext.DrawNameText("�� AI ��", "�\��������퐬���m���A2���B");
        yield return new WaitForSeconds(4.0f);

        SoundManager.instance.PlayVOICE("2-tuika");
        uitext.DrawNameText("�� AI ��", "�c�c�c�ł����A");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("2-23");
        uitext.DrawNameText("�� AI ��", "�A�i�^�ƃ��^�V�Ȃ�A�ł��܂��B\n�����ċA��܂��傤�B");
        yield return new WaitForSeconds(6.0f);

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);
        //yield return null;
    }
}
