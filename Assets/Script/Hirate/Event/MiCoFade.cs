using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiCoFade : MonoBehaviour
{
    // �~�R
    [SerializeField]
    private TextMeshProUGUI MiCo;
    // �t�F�[�h�I�u�W�F�N�g
    [SerializeField]
    private GameObject fade;
    // �X�^�[�g�������̃t���O
    private bool bStart;

    // Start is called before the first frame update
    void Start()
    {
        bStart = false;
    }

    private void Update()
    {
        if (!bStart)
        {
            StartCoroutine(MiCoFadeOut());
            bStart = true;
        }
    }


    // Update is called once per frame
    IEnumerator MiCoFadeOut()
    {
        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        MiCo.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // �t�F�[�h�A�E�g�ɂ����鎞�ԁi�b�j���ύX��
        const float MiCo_time = 3.0f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 50;

        // �E�F�C�g���ԎZ�o
        float wait_time = MiCo_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            // Alpha�l���������グ��
            Color new_color = MiCo.color;
            new_color.a = alpha / 255.0f;
            MiCo.color = new_color;
        }
        Color color = MiCo.color;
        color.a = 1.0f;
        MiCo.color = color;

        if (MiCo.color.a == 1.0f)
        {
            // �҂�����
            yield return new WaitForSeconds(3.0f);

            // ���O�̃V�[���ɓ����
            SceneNowBefore.instance.sceneBeforeCatch = SceneNowBefore.instance.sceneNowCatch;
            // �V�[���J��
            StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext_time(
                            SceneLoadStartUnload.SCENE_NAME.E_RESULT_COMPLETED,
                            SceneLoadStartUnload.SCENE_NAME.E_TITLE,
                            3.0f));
        }
    }
}
