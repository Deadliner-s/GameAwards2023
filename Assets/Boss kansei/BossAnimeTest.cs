using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimeTest : MonoBehaviour
{
    Animator anime;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            anime.SetBool("isMove", true);
            anime.SetBool("isOpen", true);
            anime.SetBool("isClose", true);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            anime.SetBool("isMove", false);
            anime.SetBool("isOpen", false);
            anime.SetBool("isClose", false);
        }
        //transform.position = pos;
    }
}
