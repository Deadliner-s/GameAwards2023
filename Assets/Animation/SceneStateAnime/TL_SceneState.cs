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
        // シーン遷移フラグの取得
        this.CameraMoveSceneP = FindAnyObjectByType<CameraMoveSceneP>();
        
        _director = GetComponent<PlayableDirector>();
        _director.played += Director_Played;
        _director.stopped += Director_Stopped;
        
        // タイムラインの再生
        _director.Play();

    }

    // タイムライン再生開始時
    void Director_Played(PlayableDirector obj)
    {
    }
    // タイムライン再生終了時
    void Director_Stopped(PlayableDirector obj)
    {
        CameraMoveSceneP.bAniEnd = true;
    }
}
