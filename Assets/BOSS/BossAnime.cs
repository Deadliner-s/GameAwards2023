using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnime : MonoBehaviour
{
    Animator anime;
    public GameObject wpobj;
    private WeakPoint wp;

    // フェイズ切り替え用
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        wp = gameObject.GetComponent<WeakPoint>();

        AtkPhaseFlg = PhaseObj.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        AtkPhaseFlg = PhaseObj.activeSelf;


        if (AtkPhaseFlg == false)
        {
            // ハイスピードフェイズ
            anime.SetBool("isMove", false);

            wp.enabled = false;
        }
        else
        {
            // アタックフェイズ
            anime.SetBool("isMove", true);

            wp.enabled = true;
        }
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    anime.SetBool("isMove", true);

        //    wp.enabled = true;
        //}

        //if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    anime.SetBool("isMove", false);

        //    wp.enabled = false;
        //}
    }
}
