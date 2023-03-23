using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterimage : MonoBehaviour
{
    [Header("オブジェクト設定")]
    [Tooltip("プレイヤー")]
    [SerializeField] GameObject player;
    [Tooltip("残像")]
    [SerializeField] GameObject afterimageObj;
    private GameObject[] obj = new GameObject[5];
    private Vector3 pos; // 現在の座標
    private Vector3[] latePos = new Vector3[5]; // 直前の座標
    private float elapsedTime; // 経過時間

    // Start is called before the first frame update
    void Start()
    {
        // 現在の座標にプレイヤー座標を代入
        pos = player.transform.position;
        // 残像を生成
        for (int i = 0; i < 5; i++)
        {
            obj[i] = Instantiate(
                afterimageObj, // 残像のオブジェクト
                new Vector3(pos.x, pos.y, pos.z), // 現在の座標
                Quaternion.identity); // 回転
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 経過時間
        //elapsedTime += Time.deltaTime;
        for (int i = 0; i < 5; i++)
        {
            if (i < 4) {
                latePos[i] = latePos[i + 1];
            }
            obj[i].transform.position = latePos[i];
            Debug.Log(obj[0].transform.position);
        }
        // 直前の座標に現在の座標を代入
        latePos[5 - 1] = pos;
        // 現在の座標にプレイヤー座標を代入
        pos = player.transform.position;
    }
}
