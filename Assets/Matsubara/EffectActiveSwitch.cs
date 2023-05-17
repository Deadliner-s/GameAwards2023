using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActiveSwitch : MonoBehaviour
{
    [Header("アクティブ状態を切り替えるエフェクト")]
    [SerializeField] GameObject EffectObj;

    private GameObject playerManager;


    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        //playermove = gameObject.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.GetComponent<PlayerMove>().maneverFlg == true)
        {
            if (playerManager.GetComponent<PlayerMove>().inputMove.y >= 0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.x >= 0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.y <= -0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.x <= -0.5f)
            {
                EffectObj.SetActive(false);
            }
        }

        if (playerManager.GetComponent<PlayerMove>().maneverFlg == false)
        {
            EffectObj.SetActive(true);
        }

    }
}
