using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMoveNext : MonoBehaviour
{
    private void Start()
    {
        SceneAccessSearch.SceneAccessCatchLoadStart(SceneNowBefore.instance.sceneBeforeCatch);
        SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_DUMMY);
    }
}
