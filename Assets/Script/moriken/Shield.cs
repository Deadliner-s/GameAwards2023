using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //Panel（メニューウィンドウ）の変数
    public GameObject Object;
    bool UseFlag;

    public float Hp;
    public float Damage;

    public float MaxInvflame;
    float Invflame;

    // 変数cubesの定義：GameObjectの配列
    GameObject[] cubes;

    // Start is called before the first frame update
    void Start()
    {
        //初期状態ではメニューを非表示
        Object.SetActive(false);
        //フラグを非表示判定
        UseFlag = false;

        // "enemy"タグが設定されたゲームオブジェクトの配列を取得し、変数cubesに格納する
        cubes = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (UseFlag == true)
        {
            //初期状態ではメニューを非表示
            Object.SetActive(true);
            Invflame++;
        }

        if(MaxInvflame < Invflame)
        {
            Object.SetActive(false);
            UseFlag = false;
            Invflame = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (UseFlag == false)
        {
            // もし衝突した相手オブジェクトのタグが"Enemy"ならば中の処理を実行
            if (collision.gameObject.CompareTag("Enemy"))
            {
                UseFlag = true;
                Hp -= Damage;
                if (Hp <= 0)
                {
                    //                this.Destroy();
                }
            }
        }
    }
}
