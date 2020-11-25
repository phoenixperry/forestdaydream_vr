using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class flower_grab : MonoBehaviour
{
    private Interactable interactable;
    public bool touchGround;
    private bool isGrabEnding;

    public GameObject bouncing_fruit;
    public int Numbering;
    public Mesh[] nextMesh;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.highlightOnHover = false;
        touchGround = true;      
    }

    void Update()
    {
        if(!touchGround && isGrabEnding)
        {
            for (int i = 0; i<3; i++)
            {
                GameObject g = Instantiate(bouncing_fruit, gameObject.transform.position, Quaternion.identity);
                g.GetComponent<fruit>().numbering = Numbering;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            gameObject.GetComponents<AudioSource>()[0].PlayDelayed(0);
            touchGround = true;
        }
        if (other.gameObject.CompareTag("flower"))
        {
            Numbering++;
            if (Numbering > 2)
            {
                Numbering -= 3;
            }
            gameObject.GetComponent<MeshFilter>().mesh = nextMesh[Numbering];
            gameObject.GetComponents<AudioSource>()[0].PlayDelayed(0);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            gameObject.GetComponents<AudioSource>()[0].PlayDelayed(0);
            touchGround = false;
        }
    }    

    private void OnHandHoverBegin(Hand hand)
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
    private void OnHandHoverEnd(Hand hand)
    {
        gameObject.GetComponent<Animator>().enabled = true;
    }
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        isGrabEnding = hand.IsGrabEnding(gameObject);
        
        //grab the object
        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
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
       
    }
}
