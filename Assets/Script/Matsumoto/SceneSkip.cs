using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSkip : MonoBehaviour
{
    private Fade fade;
    private Myproject InputActions;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Start.performed += context => OnStart();
    }

    // Start is called before the first frame update
    void Start()
    {
        fade = GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnStart()
    {
        // ÉVÅ[ÉìñºÇ≈ï™äÚ
        if (SceneManager.GetActiveScene().name == "Prologue")
        {
            InputActions.Disable();
            fade.StartCoroutine("Color_FadeOut", "Stage1");
        }
        if (SceneManager.GetActiveScene().name == "Stage2Event")
        {
            InputActions.Disable();
            fade.StartCoroutine("Color_FadeOut", "Stage2");
        }
        if (SceneManager.GetActiveScene().name == "Stage3Event")
        {
            InputActions.Disable();
            fade.StartCoroutine("Color_FadeOut", "merge_2");
        }
        if (SceneManager.GetActiveScene().name == "Epilogue")
        {
            InputActions.Disable();
            fade.StartCoroutine("Color_FadeOut", "Title");
        }
    }
}
