using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class push : MonoBehaviour
{
    private Myproject InputActions;

    private void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Select.performed += context => Select();
    }

    private void Select()
    {
        InputActions.Disable();
        SceneManager.LoadScene("Title");
    }
}
