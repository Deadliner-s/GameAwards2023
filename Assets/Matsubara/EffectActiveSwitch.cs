using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActiveSwitch : MonoBehaviour
{
    [Header("アクティブ状態を切り替えるエフェクト")]
    [SerializeField] GameObject EffectObj;

    private PlayerMove playermove;


    // Start is called before the first frame update
    void Start()
    {
        playermove = gameObject.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playermove.maneverFlg == true)
        {
            if (playermove.inputMove.y >= 0.5f ||
                playermove.inputMove.x >= 0.5f ||
                playermove.inputMove.y <= -0.5f ||
                playermove.inputMove.x <= -0.5f)
            {
                EffectObj.SetActive(false);
            }
        }

        if (playermove.maneverFlg == false)
        {
            EffectObj.SetActive(true);
        }

    }
}
