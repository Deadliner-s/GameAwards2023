using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_collision : MonoBehaviour
{
    public AudioClip audioClip;
    AudioSource audioSource;

    private CapsuleCollider Col;
    private float timer = 0.0f;    

    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<CapsuleCollider>();
        Col.enabled = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 2.0f)
        {
            Col.enabled = true;
            audioSource.Play();
        }

        if (timer >= 6.0f)
        {
            Col.enabled = false;
        }
    }
}
