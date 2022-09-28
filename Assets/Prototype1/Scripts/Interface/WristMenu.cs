using Zinnia.Action;
using UnityEngine;

public class WristMenu : MonoBehaviour
{
    [SerializeField] BooleanAction MenuButton;
    [SerializeField] GameObject Interface;
    bool MenuShown = false;
    bool MenuButtonHeld = false;

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
                Interface.SetActive(true);
            }
            else //Hide Menu
            {
                MenuShown = false;
                Interface.SetActive(false);
            }
        }
    }

    public void Hide()
    {
        MenuShown = false;
        Interface.SetActive(false);
    }
}