using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tilia.Interactions.Interactables.Interactables;
using Tilia.Interactions.Interactables.Interactors;
using UnityEngine;
using Zinnia.Action;

public class WorldGrabbable : MonoBehaviour
{
    // [SerializeField] private BooleanAction[] actions;
    
    public void Grab(InteractableFacade interactable)
    {
        GrabbableWorldTag world = interactable.GetComponent<GrabbableWorldTag>();
        // if (!world) interactable.UngrabAll();
        if (world) interactable.UngrabAll();
    }
}