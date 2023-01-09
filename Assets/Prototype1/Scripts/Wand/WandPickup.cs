using Tilia.Interactions.Interactables.Interactors;
using UnityEngine;

public class WandPickup : MonoBehaviour
{
    public bool AttachedToMenu = true;
    public void OnGrab(InteractorFacade interactor)
    {
        if (AttachedToMenu)
        {
            this.transform.parent = null;
            this.transform.localScale = interactor.transform.lossyScale;
            AttachedToMenu = false;
        }
    }
}