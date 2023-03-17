using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    Myproject InputActions;
    public GameObject PlayerMissile;    // ミサイルとして使用するオブジェクトの指定
    private Vector3 PlayerPos;                  // プレイヤーの座標
    public Vector3 ShiftPos;                   // 生成場所(プレイヤーの座標からどのくらいずらすか

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Shot.performed += context => OnShot();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnShot()
    {
        var TargetObj = GameObject.Find("RockOn");
        if (TargetObj == null)
        {
            PlayerPos = transform.position;
            PlayerPos.x += ShiftPos.x;
            PlayerPos.y += ShiftPos.y;
            PlayerPos.z += ShiftPos.z;
            // 弾を発射する処理
            GameObject obj = Instantiate(PlayerMissile, PlayerPos, Quaternion.identity);
        }        
    }
}
