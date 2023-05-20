using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyRotation : MonoBehaviour
{
    // 背景の回転速度
    [Header("背景の回転速度")]
    [Tooltip("通常")]
    [SerializeField] private float rotationSpeedNormal = -0.1f;
    [Tooltip("アタック")]
    [SerializeField] private float rotationSpeedAttack = -0.1f;
    [Tooltip("ハイスピード")]
    [SerializeField] private float rotationSpeedHighSpeed = -0.1f;

    // 背景生成オブジェクト
    private GameObject skyInstanceObj;

    // 回転
    private float rotation;      // 現在の回転
    private float rotationSpeed; // 回転速度
    private PhaseManager.Phase PhaseFlg; // フェーズフラグ
    private Material sky; // 生成したスカイボックスを入れる用

    // Start is called before the first frame update
    void Start()
    {
        // スカイボックスの取得
        SkyBoxCatch();

        // スカイボックスを取得
        //sky = RenderSettings.skybox;

        // フェーズ取得用
        try
        {
            PhaseFlg = PhaseManager.instance.GetPhase();
        }
        catch
        {

        }

        // 通常フェーズを代入
        PhaseFlg = PhaseManager.Phase.Normal_Phase;
    }

    // Update is called once per frame
    void Update()
    {
        // フェーズ取得用
        try
        {
            PhaseFlg = PhaseManager.instance.GetPhase();
        }
        catch
        {

        }

        // フェーズによって切り替え
        // 通常
        if (PhaseFlg == PhaseManager.Phase.Normal_Phase) {
            rotationSpeed = rotationSpeedNormal;
        }
        // アタック
        if (PhaseFlg == PhaseManager.Phase.Attack_Phase) {
            rotationSpeed = rotationSpeedAttack;
        }
        // ハイスピード
        if (PhaseFlg == PhaseManager.Phase.Speed_Phase) {
            rotationSpeed = rotationSpeedHighSpeed;
        }

        if (sky != null)
        {
            // 背景の回転
            rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);
            // 処理後の回転を代入
            sky.SetFloat("_Rotation", rotation);
        }
        else
        {
            Debug.Log("SkyBoxがNullです。");
        }

// デバッグ用
#if _DEBUG
        Debug.Log(rotation);
#endif // _DEBUG
    }

    // スカイボックスを取得する関数
    private void SkyBoxCatch()
    {
        // シーン検索
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ルート内のオブジェクトを検索
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SkyBoxInstance skyBoxInstance = sceneRootObj.GetComponent<SkyBoxInstance>();
            if (skyBoxInstance != null)
            {
                sky = skyBoxInstance.GetSky();
                RenderSettings.skybox = sky;
                break;
                //yield return new WaitForSeconds(0.0f);
            }
        }
    }
}
