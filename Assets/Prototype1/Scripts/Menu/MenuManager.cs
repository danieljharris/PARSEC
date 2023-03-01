using Zinnia.Action;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] BooleanAction MenuButton;
    [SerializeField] Menu MainMenu;
    [SerializeField] protected Presenter presenter;
    private Menu CurrentMenu;
    private bool MenuShown = false;
    private bool MenuButtonHeld = false;
    private bool MenuDisabled = false;

    void Start() => presenter.onPresenterChanged += OnPresenterChanged;
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
        CurrentMenu.gameObject.SetActive(true);
    }
    private void Hide()
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