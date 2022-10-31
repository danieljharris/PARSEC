using Zinnia.Action;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] BooleanAction MenuButton;
    [SerializeField] Menu MainMenu;
    Menu CurrentMenu;
    private bool MenuShown = false;
    private bool MenuButtonHeld = false;

    void Update()
    {
        // Deals with menu button being held down
        if(MenuButton.Value && MenuButtonHeld) return;
        if(!MenuButton.Value && MenuButtonHeld)
        {
            MenuButtonHeld = false;
            return;
        }

        if(MenuButton.Value)
        {
            MenuButtonHeld = true;

            if(!MenuShown) // Show Menu
            {
                MenuShown = true;
                CurrentMenu = MainMenu;
                CurrentMenu.gameObject.SetActive(true);
            }
            else //Hide Menu
            {
                MenuShown = false;
                CurrentMenu.gameObject.SetActive(false);
            }
        }
    }

    public void Hide()
    {
        if(!MenuShown) return;

        MenuShown = false;
        CurrentMenu.gameObject.SetActive(false);
    }

    public void Switch(Menu newMenu)
    {
        CurrentMenu.gameObject.SetActive(false);
        CurrentMenu = newMenu;
        CurrentMenu.gameObject.SetActive(true);
    }
}