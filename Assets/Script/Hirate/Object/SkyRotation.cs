using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // 背景のマテリアル
    [Header("背景マテリアル設定")]
    [Tooltip("背景のマテリアル")]
    public Material sky;

    // 回転
    private float rotation;      // 現在の回転
    private float rotationSpeed; // 回転速度
    private PhaseManager.Phase PhaseFlg; // フェーズフラグ
    private Material skyInstance; // 生成したスカイボックスを入れる用

    // Start is called before the first frame update
    void Start()
    {
        skyInstance = new Material(sky); // 生成したスカイボックスを入れる用
        RenderSettings.skybox = skyInstance;

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

        // 背景の回転
        rotation = Mathf.Repeat(skyInstance.GetFloat("_Rotation") + rotationSpeed, 360f);
        // 処理後の回転を代入
        skyInstance.SetFloat("_Rotation", rotation);
// デバッグ用
#if _DEBUG
        Debug.Log(rotation);
#endif // _DEBUG
    }

    // オブジェクトが破棄された時実行
    private void OnDestroy()
    {
        if(skyInstance != null)
        {
            Destroy(skyInstance);
            skyInstance = null;
        }
    }
}
