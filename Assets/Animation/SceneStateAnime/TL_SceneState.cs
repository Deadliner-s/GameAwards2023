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
        // シーン遷移フラグの取得
        sceneEventMove = FindAnyObjectByType<SceneEventMove>();

        _director = GetComponent<PlayableDirector>();
        _director.played += Director_Played;
        _director.stopped += Director_Stopped;
    }

    private void Update()
    {
        if (sceneEventMove == null)
        {
            // シーン遷移フラグの取得
            sceneEventMove = FindAnyObjectByType<SceneEventMove>();
        }

        if (sceneEventMove.bTextEnd == true)
        {
            // タイムラインの再生
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

    // タイムライン再生開始時
    void Director_Played(PlayableDirector obj)
    {
    }
    // タイムライン再生終了時
    void Director_Stopped(PlayableDirector obj)
    {
        
    }
}
