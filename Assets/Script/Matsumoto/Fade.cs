using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ////�t�F�[�h�C��
        //if(SceneManager.GetActiveScene().name == "Prologue")
        //{
        //    StartCoroutine("Color_FadeIn");
        //}
        //if(SceneManager.GetActiveScene().name == "Stage2Event")
        //{
        //    StartCoroutine("Color_FadeIn");
        //}
        //if(SceneManager.GetActiveScene().name == "Stage3Event")
        //{
        //    StartCoroutine("Color_FadeIn");
        //}
        //if (SceneManager.GetActiveScene().name == "Title")
        //{
        //    StartCoroutine("Color_FadeIn");
        //}
        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_TITLE)
        {
            StartCoroutine("Color_FadeIn");
        }
        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE)
        {
            StartCoroutine("Color_FadeIn");
        }
        // �R���e�B�j���[�̏ꍇ
        if(GManager.instance.GetContinueFlg() == true)
        {
            if(SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1)
            {
                StartCoroutine("Color_FadeIn");
            }
            if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2)
            {
                StartCoroutine("Color_FadeIn");
            }
            if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
            {
                StartCoroutine("Color_FadeIn");
            }
            GManager.instance.SetContinueFlg(false);
        }

        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_RESULT_FAILED)
        {
            StartCoroutine("Color_FadeIn");
        }
    }

    public IEnumerator Color_FadeIn()
    {
        // ��ʂ��t�F�[�h�C��������R�[���`��
        // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h���̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // �t�F�[�h�C���ɂ����鎞�ԁi�b�j���ύX��
        const float fade_time = 1.0f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 50;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // �҂�����
            yield return new WaitForSecondsRealtime(wait_time);

            // Alpha�l��������������
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }

    public IEnumerator Color_FadeIn_wait(float time)
    {
        // ��ʂ��t�F�[�h�C��������R�[���`��
        // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h���̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // �҂�����
        yield return new WaitForSecondsRealtime(2.0f);

        // �t�F�[�h�C���ɂ����鎞�ԁi�b�j���ύX��
        float fade_time = time;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 50;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // �҂�����
            yield return new WaitForSecondsRealtime(wait_time);

            // Alpha�l��������������
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }

    public IEnumerator Color_FadeOut()
    {
        // ��ʂ��t�F�[�h�C��������R�[���`��
        // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // �t�F�[�h�A�E�g�ɂ����鎞�ԁi�b�j���ύX��
        const float fade_time = 1.0f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 50;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSecondsRealtime(wait_time);

            // Alpha�l���������グ��
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {
            // �C�x���g�V�[�����X�L�b�v���ꂽ���ɗ���Ă���{�C�X���~�߂�
            SoundManager.instance.StopVOICE();
            // �V�[���J��
            //SceneManager.LoadScene(nextScene);
            // SceneLoadManager���^�O����
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // �V�[���̊J�n
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();
            
        }
    }

    public IEnumerator Color_FadeOut_Title(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // ��ʂ��t�F�[�h�C��������R�[���`��
        // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // �t�F�[�h�A�E�g�ɂ����鎞�ԁi�b�j���ύX��
        const float fade_time = 1.0f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 50;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSecondsRealtime(wait_time);

            // Alpha�l���������グ��
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {
            // �V�[���J��
            SceneAccessSearch.SceneAccessCatchLoad(scene_name);
            SceneAccessSearch.SceneAccessCatchStart();
            SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_TITLE);
        }
    }

    public IEnumerator Color_FadeOut_GameOver(
        SceneLoadStartUnload.SCENE_NAME scene_now)
    {
        // ��ʂ��t�F�[�h�C��������R�[���`��
        // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // �t�F�[�h�A�E�g�ɂ����鎞�ԁi�b�j���ύX��
        const float fade_time = 1.0f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 50;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSecondsRealtime(wait_time);

            // Alpha�l���������グ��
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {
            // �V�[���J��
            SceneAccessSearch.SceneAccessCatchLoad(SceneLoadStartUnload.SCENE_NAME.E_RESULT_FAILED);
            SceneAccessSearch.SceneAccessCatchStart();
            SceneAccessSearch.SceneAccessCatchUnload(scene_now);
        }
    }

    public IEnumerator Color_FadeOut_NowNext(
        SceneLoadStartUnload.SCENE_NAME scene_now,
        SceneLoadStartUnload.SCENE_NAME scene_next)
    {
        // ��ʂ��t�F�[�h�C��������R�[���`��
        // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // �t�F�[�h�A�E�g�ɂ����鎞�ԁi�b�j���ύX��
        const float fade_time = 1.0f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 50;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSecondsRealtime(wait_time);

            // Alpha�l���������グ��
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {
            // �|�[�Y����V�[���J�ڂ��鎞
            if (Time.timeScale != 1.0f)
            {
                // �ꎞ��~���̃T�E���h���~������
                SoundManager.instance.StopAll();
                // �t�F�[�h�A�E�g���Ă��玞���Ďn��������
                Time.timeScale = 1.0f;
                // �t�F�[�h�C������n�߂邽�߂ɃR���e�B�j���[�t���O�𗧂Ă�
                GManager.instance.SetContinueFlg(true);
            }
            // �V�[���J��
            SceneAccessSearch.SceneAccessCatchLoad(scene_next);
            SceneAccessSearch.SceneAccessCatchStart();
            SceneAccessSearch.SceneAccessCatchUnload(scene_now);
        }
    }

    public IEnumerator Color_FadeOut_NowNext_time(
        SceneLoadStartUnload.SCENE_NAME scene_now,
        SceneLoadStartUnload.SCENE_NAME scene_next,
        float time)
    {
        // ��ʂ��t�F�[�h�C��������R�[���`��
        // �O��F��ʂ𕢂�Panel�ɃA�^�b�`���Ă���

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // �t�F�[�h�A�E�g�ɂ����鎞�ԁi�b�j���ύX��
        float fade_time = time;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 50;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSecondsRealtime(wait_time);

            // Alpha�l���������グ��
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {
            // �V�[���J��
            SceneAccessSearch.SceneAccessCatchLoad(scene_next);
            SceneAccessSearch.SceneAccessCatchStart();
            SceneAccessSearch.SceneAccessCatchUnload(scene_now);
        }
    }
}
