using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
   // public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void ToSampleScene()
    {
        SceneManager.LoadScene("merge_2");
    }

}

