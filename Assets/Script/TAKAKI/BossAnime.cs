using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnime : MonoBehaviour
{
    Animator anime;

    public GameObject wpobj;//翼
    public GameObject weakTop;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;
    private BossAnimeWeakTop top;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

        //weakanime = weakobj.GetComponent<Animator>();

        currntPhase = PhaseManager.instance.GetPhase();
        nextPhase = currntPhase;

        top = weakTop.GetComponent<BossAnimeWeakTop>();
    }

    // Update is called once per frame
    void Update()
    {
        currntPhase = PhaseManager.instance.GetPhase();
        //Debug.Log(gameObject.transform.position);
        if (nextPhase != currntPhase)
        {
            nextPhase = currntPhase;
            if (currntPhase == PhaseManager.Phase.Normal_Phase)
            {
                anime.SetBool("isWing", false);
                anime.SetBool("isBinder", false);

                SoundManager.instance.PlaySE("BossAnime");
            }
            else if (currntPhase == PhaseManager.Phase.Speed_Phase)
            {
                // ハイスピードフェイズ
                anime.SetBool("isWing", true);

                SoundManager.instance.PlaySE("BossAnime");
            }
            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // アタックフェイズ
                anime.SetBool("isBinder", true);
            }
        }

        if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            if (top.obj == null)
            {
                anime.SetBool("isBinder", false);
            }
        }
    }
}
