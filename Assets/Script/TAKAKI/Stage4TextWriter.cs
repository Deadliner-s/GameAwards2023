using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4TextWriter : MonoBehaviour
{
    public Stage4UIText uitext;

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
        yield return new WaitForSeconds(7.0f);

        StartCoroutine("WindowScaleUp");

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

        StartCoroutine("WindowScaleDown");

        // SceneMoveManager���^�O����
        GameObject obj = GameObject.FindGameObjectWithTag("NowEventSceneSet");
        // �V�[���̊J�n
        obj.GetComponent<SceneEventMove>().bTextEnd = true;
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
