using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1TextWriter : MonoBehaviour
{
    public Stage1UIText uitext;

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;

    private float Max_Height = 2.756843f;

    // Start is called before the first frame update
    void Start()
    {
        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);

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
        StartCoroutine("WindowScaleUp");

        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("0-1");
        uitext.DrawNameText("�� �i�ߊ� ��", "���߂č����e��`����");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("0-2");
        uitext.DrawNameText("�� �i�ߊ� ��", "���͂ȃr�[������u�q���O���C�v�ɂ��A�G����\n��s���̂����Ă���̂��{�~�b�V�����̖ړI�ł���B");
        yield return new WaitForSeconds(8.0f);

        SoundManager.instance.PlayVOICE("0-3");
        uitext.DrawNameText("�� �i�ߊ� ��", "�u�q���O���C�v�𓖂Ă邽�߂ɂ́A���m�ȏƏ���\n�G�l���M�[�[�U�܂Ń��c�ɋC�Â���Ȃ��K�v������B");
        yield return new WaitForSeconds(8.0f);

        SoundManager.instance.PlayVOICE("0-4");
        uitext.DrawNameText("�� �i�ߊ� ��", "���c�ɐڋ߂��Ă̏Ə��⍲�ƁA���ӂ�����\n�����Ă��炤���Ƃ��N�ɉۂ���ꂽ�C�����B");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("0-5");
        uitext.DrawNameText("�� �i�ߊ� ��", "�B��A������\�ɂ���̂��N�̓���\n���Ă���u�u���[�A�T���g�v�ł���B");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("0-6");
        uitext.DrawNameText("�� �i�ߊ� ��", "�՗�ȍU�����\�z����邪�A�����퓬�ƃo���A�t�B�[���h��\n���ڂ��邻�̋@�̂ł���ΐ؂蔲���邱�Ƃ��\�Ȃ͂����B");
        yield return new WaitForSeconds(9.5f);

        SoundManager.instance.PlayVOICE("0-7");
        uitext.DrawNameText("�� �i�ߊ� ��", "���c�̒��ӂ��Ђ����߁A��b�ł������퓬���p������B");
        yield return new WaitForSeconds(5.5f);

        SoundManager.instance.PlayVOICE("0-8");
        uitext.DrawNameText("�� �i�ߊ� ��", "�ȏゾ�B�������F��B");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("0-9");
        uitext.DrawNameText("�� AI ��", "�܂��Ȃ��G�����s���̂̍U������\n�ɓ˓����܂��B���ӂ��Ă��������B");
        yield return new WaitForSeconds(6.0f);

        StartCoroutine("WindowScaleDown");
        //yield return null;
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
