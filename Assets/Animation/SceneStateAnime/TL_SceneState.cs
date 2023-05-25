using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TL_SceneState : MonoBehaviour
{
    
    private PlayableDirector _director;
    private SceneEventMove sceneEventMove;

    // Start is called before the first frame update
    void Start()
    {   
        // �V�[���J�ڃt���O�̎擾
        sceneEventMove = FindAnyObjectByType<SceneEventMove>();

        _director = GetComponent<PlayableDirector>();
        _director.played += Director_Played;
        _director.stopped += Director_Stopped;
    }

    private void Update()
    {
        if (sceneEventMove == null)
        {
            // �V�[���J�ڃt���O�̎擾
            sceneEventMove = FindAnyObjectByType<SceneEventMove>();
        }

        if (sceneEventMove.bTextEnd == true)
        {
            // �^�C�����C���̍Đ�
            _director.Play();
        }

        if (_director.extrapolationMode != DirectorWrapMode.Hold)
        {
            return; 
        }
        if (_director.time >= _director.duration)
        {
            sceneEventMove.bAniEnd = true;
        }
    }

    // �^�C�����C���Đ��J�n��
    void Director_Played(PlayableDirector obj)
    {
    }
    // �^�C�����C���Đ��I����
    void Director_Stopped(PlayableDirector obj)
    {
        
    }
}
