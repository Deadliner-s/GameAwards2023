using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove_new : MonoBehaviour
{
    [SerializeField]
    public GameObject player;               // プレイヤーのオブジェクト
    private Vector3 playerPos;

    [Header("カメラが動き始める時のプレイヤーの位置")]
    public float StartMoveTop = 1.2f;       // カメラが動き始める時のプレイヤーの位置
    public float StartMoveBottom = 0.5f;

    [Header("カメラの移動上限")]
    public float LimitTop = 2.0f;           // カメラの移動上限
    public float LimitBottom = -0.8f;


    private Vector3 nextPosition;   // 移動後の位置
    private Vector3 initialPos;     // カメラの初期位置

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = transform.position; ;
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        nextPosition = this.transform.position;
        playerPos = player.transform.position;

        // Playerが移動した分、カメラも移動する
        if (playerPos.y <= LimitTop)
        {
            if (playerPos.y >= StartMoveTop)
            {
                nextPosition.y = initialPos.y + playerPos.y - StartMoveTop;
            }
        }
        if (playerPos.y >= LimitBottom)
        {
            if (playerPos.y <= StartMoveBottom)
            {
                nextPosition.y = initialPos.y + playerPos.y - StartMoveBottom;
            }
        }

        // 移動
        transform.position = nextPosition;
    }
}
