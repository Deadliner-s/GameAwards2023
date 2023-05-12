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

        string A = "�@�@�@�@�@�@�@�@�@�@";

        uitext.DrawNameText("", " ");
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "�G�����s���̂̔j����m�F�B" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�i�ߊ�", "�������I " + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("�i�ߊ�", "�悭������B���O�͖{���� �p�Y��" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "���I���̏��F�A�m�F���܂����B�A�҂��܂��傤�B" + A);
        yield return new WaitForSeconds(3.0f);

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);
        //yield return null;
    }
}
