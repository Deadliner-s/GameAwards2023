using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{
    Myproject InputActions;

    public List<Animator> menuList;

    public int MaxMenu;

    public int currentMenu;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Up.performed += context => OnUp();
        InputActions.UI.Down.performed += context => OnDown();
        InputActions.UI.Select.performed += context => OnSelect();
        InputActions.UI.Back.performed += context => OnSelect();

        currentMenu = 0;
        for (int i = 0; i < MaxMenu; i++)
        {
            if (i == 0)
            {
                menuList[currentMenu].SetTrigger("OnCursor");
                continue;
            }
            menuList[i].SetTrigger("Reset");
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

    public void OnUp()
    {
        menuList[currentMenu].SetTrigger("Reset");
        currentMenu--;
        if (currentMenu < 0)
        {
            currentMenu = MaxMenu - 1;
        }
        menuList[currentMenu].SetTrigger("OnCursor");
    }

    public void OnDown()
    {
        menuList[currentMenu].SetTrigger("Reset");
        currentMenu++;
        if (currentMenu == MaxMenu)
        {
            currentMenu = 0;
        }
        menuList[currentMenu].SetTrigger("OnCursor");
    }

    public void OnSelect()
    {
        menuList[currentMenu].SetBool("IsSelect", true);

        switch (currentMenu)
        {
            case (0):
                // NEW GAME
                SceneManager.LoadScene("merge_2");
                break;
            case (1):
                // CONTINUE
                break;
            case (2):
                // OPTION
                break;

        }
    }

    public void OnBack()
    {
        menuList[currentMenu].SetBool("IsSelect", false);
    }
}
