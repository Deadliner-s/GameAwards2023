using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public UIText uitext;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Cotest");

        nextPhase = currntPhase;

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
        currntPhase = PhaseManager.instance.GetPhase();

        //uitext.DrawText("�i���[�V�����������炱�̂܂܏�����OK");
        //yield return StartCoroutine("Skip");

        //uitext.DrawText("���O", "�l���b���̂Ȃ炱��Ȋ���");
        //yield return StartCoroutine("Skip");
        yield return null;
    }
}
