using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnime : MonoBehaviour
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
        anime.SetBool("isMove", true);
    }
}
