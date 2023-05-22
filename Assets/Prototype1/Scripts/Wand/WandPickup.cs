using Tilia.Interactions.Interactables.Interactors;
using UnityEngine;
using UnityEngine.Events;

public class WandPickup : MonoBehaviour
{
    public bool AttachedToMenu = true;
    public UnityEvent OnPickup = new UnityEvent();
    public void OnGrab(InteractorFacade interactor)
    {
        if (AttachedToMenu)
        {
            this.transform.parent = null;
            this.transform.localScale = interactor.transform.lossyScale;
            AttachedToMenu = false;
            OnPickup.Invoke();
        }
    }
    public void Hide()
    {
        if (AttachedToMenu)
        {
            gameObject.SetActive(false);
        }
    }
}