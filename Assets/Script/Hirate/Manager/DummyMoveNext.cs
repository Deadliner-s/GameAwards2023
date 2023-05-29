using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMoveNext : MonoBehaviour
{
    private void Start()
    {
        // リトライ時
        if (SceneSelectCatch.instance.selectCatch == 1)
        {
            // シーン遷移
            SceneAccessSearch.SceneAccessCatchLoadStart(SceneNowBefore.instance.sceneBeforeCatch);
            SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_DUMMY);
            return;
        }
        // リスタート時(ステージ1に戻る)
        if (SceneSelectCatch.instance.selectCatch == 2)
        {
            // ステージ1のみ同じシーンに戻る
            if (SceneNowBefore.instance.sceneBeforeCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1)
            {
                // シーン遷移
                SceneAccessSearch.SceneAccessCatchLoadStart(SceneNowBefore.instance.sceneBeforeCatch);
                SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_DUMMY);
                return;
            }
            else
            {
                // シーン遷移
                SceneAccessSearch.SceneAccessCatchLoadStart(SceneLoadStartUnload.SCENE_NAME.E_STAGE1);
                SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_DUMMY);
                return;
            }
        }
    }
}
