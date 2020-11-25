using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class climb : MonoBehaviour
{
    private Interactable interactable;
    private static Transform startTrasform;
    private Transform prev_hand_pos;
    private Vector3 start_pos;
    private Quaternion start_rot;

    private Vector3 playerMove;

  //  public GameObject playerplayer;


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        //startTrasform = gameObject.transform;      
        start_pos = gameObject.transform.position;
        start_rot = gameObject.transform.rotation;
    }

    void OnHandHoverBegin(Hand hand)
    {
        prev_hand_pos = hand.transform;
    }
    void OnHandHoverEnd(Hand hand)
    {

    }
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        //grab the object
        if (interactable.attachedToHand == null && grabType == GrabTypes.Grip)
        {
            Hand.AttachmentFlags A =  Hand.AttachmentFlags.ParentToHand| Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

            hand.AttachObject(gameObject, grabType, A);
            hand.HoverLock(interactable);

        }        //Release
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }

    }
    void Update()
    {
        Player.instance.transform.Translate((gameObject.transform.position- start_pos)*-0.01f);   
        gameObject.transform.position = start_pos;
        gameObject.transform.rotation = start_rot;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Tree_trunk"))
    //    {
    //        Player.instance.transform.Translate();
    //    }
    //}
}
