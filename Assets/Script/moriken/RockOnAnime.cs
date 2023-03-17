using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOnAnime : MonoBehaviour
{
    private Animator anime;


    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool flg = RockOnMarker.instance.RockOnAnime();
        //if (flg)
        //{
        //    anime.SetBool("isRockOn", true);
        //}
    }
}
