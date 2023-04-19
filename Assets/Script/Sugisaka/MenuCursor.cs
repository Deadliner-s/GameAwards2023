using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{
    private Myproject InputActions;

    private int Selected = 0;
    private int ItemMax;

    private bool Actionflg;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Up.performed += context => OnUp();
        InputActions.UI.Down.performed += context => OnDown();
        InputActions.UI.Select.performed += context => OnSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        // ÉÅÉjÉÖÅ[ÇÃêîÇéÊìæ
        ItemMax = transform.childCount;
        // èâä˙ê›íË
        SelectItem();

        Actionflg = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectItem()
    {
        transform.GetChild(Selected).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(Selected).GetChild(1).gameObject.SetActive(false);
    }

    void DeSelectItem()
    {
        transform.GetChild(Selected).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(Selected).GetChild(1).gameObject.SetActive(true);
    }



    private void OnUp()
    {
        if (Actionflg == false)
        {
            Actionflg = true;
            DeSelectItem();
            Selected--;
            Selected = (int)Mathf.Repeat(Selected, ItemMax);
            SelectItem();
            Actionflg = false;
        }
    }

    private void OnDown()
    {
        if (Actionflg == false)
        {
            Actionflg = true;
            DeSelectItem();
            Selected++;
            Selected = (int)Mathf.Repeat(Selected, ItemMax);
            SelectItem();
            Actionflg = false;
        }
    }

    private void OnSelect()
    {
        switch (Selected)
        {
            case (0):
                // NEW GAME
                SceneManager.LoadScene("merge_2");
                InputActions.Disable();
                break;
            case (1):
                // CONTINUE
                break;
            case (2):
                // OPTION
                break;

        }
    }
}
