using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public MessageText uitext;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Cotest");
    }

    // �N���b�N�҂��̃R���[�`��
    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        while (!uitext.IsClicked()) yield return 0;
    }

    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        uitext.DrawText("�i���[�V�����������炱�̂܂܏�����OK");
        yield return StartCoroutine("Skip");

        uitext.DrawText("���O", "�l���b���̂Ȃ炱��Ȋ���");
        yield return StartCoroutine("Skip");

    }
}