using UnityEngine;

public class MoveToInitialPosition : MonoBehaviour
{
    private GameObject player;          // プレイヤー

    public Transform initialTransform;  // 初期位置
    public float duration = 2.0f;       // 移動にかける時間

    private Vector3 startPosition;      // 移動開始時の位置
    private Quaternion startRotation;   // 移動開始時の回転
    private float startTime;            // 時間

    private void Start()
    {
        // プレイヤーの取得
        player = GameObject.Find("Player");

        // プレイヤーの移動を停止
        this.gameObject.GetComponent<PlayerMove>().enabled = false;
        this.gameObject.GetComponent<PlayerMoveAngle>().enabled = false;

        // 初期位置を設定
        startPosition = player.transform.position;
        startRotation = player.transform.rotation;

        // 時間を初期化
        startTime = Time.time;

        // 入力を無効化
        InputManager.instance.InputActions.Player.Disable();
    }

    private void Update()
    {
        // 経過時間を取得
        float elapsedTime = Time.time - startTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        // 移動
        player.transform.position = Vector3.Lerp(startPosition, initialTransform.position, t);
        // 回転
        player.transform.rotation = Quaternion.Lerp(startRotation, initialTransform.rotation, t);

        // 指定した秒数経過後に初期位置に到達したら処理を終了
        if (t >= 1f)
        {
            // 到達後の処理をここに追加

            enabled = false; // スクリプトを無効化して停止
        }
    }
}
