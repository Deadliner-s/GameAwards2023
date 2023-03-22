using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnime : MonoBehaviour
{
    Animator anime;
    public GameObject wpobj;
    private WeakPoint wp;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        wp = gameObject.GetComponent<WeakPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            anime.SetBool("isMove", true);

            wp.enabled = true;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            anime.SetBool("isMove", false);

            wp.enabled = false;
        }
    }
}
