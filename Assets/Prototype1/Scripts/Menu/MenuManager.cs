using System.Collections.Generic;
using Zinnia.Action;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private BooleanAction MenuButton;
    [SerializeField] private Menu MainMenu;
    [SerializeField] private List<Menu> menus;
    [SerializeField] private Presenter presenter;
    private Menu CurrentMenu;
    private bool MenuShown = false;
    private bool MenuButtonHeld = false;
    private bool MenuDisabled = false;

    void Start()
    {
        Presenter.onPresenterChanged += OnPresenterChanged;

        foreach (Menu menu in menus)
            menu.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if(MenuDisabled) return;

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
            if(!MenuShown)
                Show(MainMenu);
            else
                Hide();
        }
    }

    private void Show(Menu menu)
    {
        MenuShown = true;
        CurrentMenu = menu;
        CurrentMenu.transform.localScale = new Vector3(0.400000006f, 0.400000006f, 0.00499999989f);
    }
    private void Hide()
    {
        if(!MenuShown) return;
        MenuShown = false;
        CurrentMenu.transform.localScale = Vector3.zero;
    }
    public void Switch(Menu newMenu)
    {
        CurrentMenu.transform.localScale = Vector3.zero;
        CurrentMenu = newMenu;
        CurrentMenu.transform.localScale = new Vector3(0.400000006f, 0.400000006f, 0.00499999989f);
    }

    public void OnPresenterChanged()
    {
        // If another user is the presenter, disable the menu
        if(Presenter.IsAnyPresenter && !presenter.IsPresenter)
        {
            MenuDisabled = true;
            Hide();
        }
        else
        {
            MenuDisabled = false;
        }
    }
}