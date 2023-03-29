using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimeTest : MonoBehaviour
{
    Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            anime.SetBool("isMove", true);
            anime.SetBool("isOpen", true);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            anime.SetBool("isMove", false);
        }

        if(Input.GetKey(KeyCode.Alpha3))
        {
            anime.SetBool("isMove", true);
        }
    }
}
