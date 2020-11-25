using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class simple_grab : MonoBehaviour
{
    public Material MaterialWhenGrab;
    private Interactable interactable;
    private Vector3 handPos;
    private Vector3 handTranslate;
    private Material material_Original;
    private Color color_Original;
    
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.highlightOnHover = false;
        material_Original = gameObject.GetComponent<Renderer>().material;
    }

    private void OnHandHoverBegin(Hand hand)
    {
        gameObject.GetComponent<Animator>().enabled = false;
        // hand.ShowGrabHint();
        hand.HideGrabHint();
        gameObject.GetComponent<Renderer>().material = MaterialWhenGrab;   // .SetColor("_BaseColor", a);
    }
    private void OnHandHoverEnd(Hand hand)
    {
        gameObject.GetComponent<Animator>().enabled = true;
        //  hand.HideGrabHint();
        gameObject.GetComponent<Renderer>().material = material_Original;

    }
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding= hand.IsGrabEnding(gameObject);
        //grab the object
        if (interactable.attachedToHand == null && grabType == GrabTypes.Pinch)
        {
            Hand.AttachmentFlags A = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

            hand.AttachObject(gameObject, grabType, A);
            hand.HoverLock(interactable);
        }
        //Release
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
        this.transform.rotation = Quaternion.identity;
    }
}
