using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    public bool Stage1;
    public bool Stage2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Stage1 = false;
        Stage2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReSetData()
    {
        Stage1 = false;
        Stage2 = false;
    }

    // メニューのコンテニューで使用
    public int GetNowStage()
    {
        // 初期化
        int num = 0;

        // クリアフラグを確認して次のステージ選択
        if (Stage1 == false)
        {
            // ステージ1
            num = 0;
        }
        else if (Stage2 == false)
        {
            // ステージ2
            num = 1;
        }
        else
        {
            // ステージ3
            num = 2;
        }

        return num;
    }

    // ステージクリア時に
    public void SetClearFlg(int num)
    {
        switch (num)
        {
            case (1):
                Stage1 = true;
                break;
            case (2):
                Stage2 = true;
                break;
            default:
                break;
        }
    }
}
