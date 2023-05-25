using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TL_SceneState : MonoBehaviour
{
    
    private PlayableDirector _director;
    private CameraMoveSceneP CameraMoveSceneP; 

    // Start is called before the first frame update
    void Start()
    {   
        // �V�[���J�ڃt���O�̎擾
        this.CameraMoveSceneP = FindAnyObjectByType<CameraMoveSceneP>();
        
        _director = GetComponent<PlayableDirector>();
        _director.played += Director_Played;
        _director.stopped += Director_Stopped;
        
        // �^�C�����C���̍Đ�
        _director.Play();

    }

    private void Update()
    {
        if (_director.extrapolationMode != DirectorWrapMode.Hold)
        { return; }
        if (_director.time >= _director.duration)
        {
            CameraMoveSceneP.bAniEnd = true;
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
