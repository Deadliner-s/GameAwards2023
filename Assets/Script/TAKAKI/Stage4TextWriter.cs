using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4TextWriter : MonoBehaviour
{
    public Stage4UIText uitext;

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

        SoundManager.instance.PlayVOICE("3-1");
        uitext.DrawNameText("�� AI ��", "�G�����s���̂̔j����m�F�B");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("3-2");
        uitext.DrawNameText("�� �i�ߊ� ��", "�������I ");
        yield return new WaitForSeconds(1.5f);

        SoundManager.instance.PlayVOICE("3-3");
        uitext.DrawNameText("�� �i�ߊ� ��", "�悭������B���O�͖{���̉p�Y���I");
        yield return new WaitForSeconds(5.5f);

        SoundManager.instance.PlayVOICE("3-4");
        uitext.DrawNameText("�� AI ��", "���I���̏��F�A�m�F���܂����B�A�҂��܂��傤�B");
        yield return new WaitForSeconds(5.0f);

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);
        //yield return null;
    }
}
