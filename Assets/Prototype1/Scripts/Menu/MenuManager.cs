using Fusion;
using UnityEngine;
using System.Collections.Generic;
using Zinnia.Action;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private BooleanAction MenuButton;
    [SerializeField] private List<Menu> menus;
    [SerializeField] private Presenter presenter;
    private Menu CurrentMenu;
    private bool MenuShown = false;
    private bool MenuButtonHeld = false;
    private bool MenuDisabled = false;
    private bool isRemotePlayer = false;

    void Start()
    {
        Presenter.onPresenterChanged += OnPresenterChanged;

        foreach (Menu menu in menus)
            menu.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (isRemotePlayer || MenuDisabled) return;

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
            {
                Show(menus[0]);
            }
            else
            {
                Hide();
            }
        }
    }

    private void Show(Menu menu = null)
    {

        MenuShown = true;
        CurrentMenu = menu;
        CurrentMenu.transform.localScale = new Vector3(0.400000006f, 0.400000006f, 0.00499999989f);
        CurrentMenu.Show.Invoke();
    }
    private void Hide()
    {
        if(!MenuShown) return;
        MenuShown = false;
        CurrentMenu.transform.localScale = Vector3.zero;
        CurrentMenu.Hide.Invoke();
    }
    public void Switch(Menu newMenu)
    {
        CurrentMenu.transform.localScale = Vector3.zero;
        CurrentMenu = newMenu;
        CurrentMenu.transform.localScale = new Vector3(0.400000006f, 0.400000006f, 0.00499999989f);
    }

    public void SetRemotePlayer(bool isRemote)
    {
        isRemotePlayer = isRemote;
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