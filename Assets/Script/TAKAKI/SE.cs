using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SE : MonoBehaviour
{
    public AudioClip Soundeffeect;
    AudioSource audioSource;

    GameAwards2023 test;
    void Awake()
    {
        test = new GameAwards2023();
        test.Enable();

        test.Player.Fire.performed += context => OnFire();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            audioSource.PlayOneShot(Soundeffeect);
        }
    }

    public void OnFire()
    {
        //audioSource.
        audioSource.PlayOneShot(Soundeffeect);
        Debug.Log("test");
    }
}