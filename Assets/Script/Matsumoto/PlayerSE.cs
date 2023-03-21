using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSE : MonoBehaviour
{
    public AudioClip MoveSE;
    public AudioClip ShotSE;
    public AudioClip ManeverSE;

    private AudioSource audioSource;

    private Vector2 inputMove;
    private Myproject InputSE;

    private bool playSeFlg = false;
    void Awake()
    {
        InputSE = new Myproject();
        InputSE.Enable();

        InputSE.Player.Shot.performed += context => OnShot();
        InputSE.Player.Manever.performed += context => OnManever();

    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MoveSE;
    }

    // Update is called once per frame
    void Update()
    {
        inputMove = InputSE.Player.Move.ReadValue<Vector2>();



        if (0.0f < inputMove.x) 
        {
            if (playSeFlg == false)
            {
                audioSource.Play();
                playSeFlg = true;
            }
        }
        if (0.0f > inputMove.x)
        {
            if (playSeFlg == false)
            {
                audioSource.Play();
                playSeFlg = true;
            }
        }
        if (0.0f < inputMove.y)
        {
            if (playSeFlg == false)
            {
                audioSource.Play();
                playSeFlg = true;
            }
        }
        if (0.0f > inputMove.y)
        {
            if (playSeFlg == false)
            {
                audioSource.Play();
                playSeFlg = true;
            }
        }
        if (0.0f == inputMove.x && 0.0f == inputMove.y && playSeFlg == true)  
        {
            audioSource.Stop();
            playSeFlg = false;
        }

    }

    public void OnShot()
    {
        audioSource.PlayOneShot(ShotSE);
    }

    private void OnManever()
    {
        audioSource.PlayOneShot(ManeverSE);
    }
}