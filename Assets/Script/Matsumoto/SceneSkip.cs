using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSkip : MonoBehaviour
{
    private Myproject InputActions;

    void Awake()
    {
        if (this.gameObject != null)
        {
            InputActions = new Myproject();
            InputActions.Enable();
            InputActions.UI.Start.performed += context => OnStart();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnStart()
    {

        // ÉVÅ[ÉìñºÇ≈ï™äÚ
        if (this.gameObject != null)
        {
            InputActions.Disable();
            if (SceneManager.GetActiveScene().name == "Prologue")
            {
                GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage1");
            }
            if (SceneManager.GetActiveScene().name == "Stage2Event")
            {
                GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2");
            }
            if (SceneManager.GetActiveScene().name == "Stage3Event")
            {
                GetComponent<Fade>().StartCoroutine("Color_FadeOut", "merge_2");
            }
            if (SceneManager.GetActiveScene().name == "Epilogue")
            {
                GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Title");
            }
        }
    }
}
