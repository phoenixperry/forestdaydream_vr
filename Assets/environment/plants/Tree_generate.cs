using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Tree_generate : MonoBehaviour
{
    public GameObject cameraVR;
    public GameObject camera_;
    public GameObject cube;
    public GameObject tree_whole;
    private float timer;
    private Interactable interactable;
    //private List<Vector3> pos_list;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();

        //list of branch positions
        var pos_list = new List<Vector3>();
        pos_list.Add(new Vector3(3, 5, 1));
        pos_list.Add(new Vector3(2,4,5));
        pos_list.Add(new Vector3(4,2,2));
        Debug.Log(pos_list);

    }
    private void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
        camera_.SetActive(true);
        cameraVR.SetActive(false);
       
    }

    //-------------------------------------------------
    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
        cameraVR.SetActive(true );
        camera_.SetActive(false );
    }

    private void HandHoverUpdate(Hand hand)
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            BranchGeneraion();
            timer = 0;
        }


        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);
        //grab the object
        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
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

    private void OnMouseOver()
    {
        timer += Time.deltaTime;
        Debug.Log("mouseOn");
    }
   
    void BranchGeneraion()
    {
        GameObject cube_leaves = Instantiate(cube, this.transform.position, Quaternion.identity);
        cube_leaves.transform.parent = this.transform;
        
        cube_leaves.transform.localScale = new Vector3(1, 1, 1) * Random.Range(0.2f, 0.7f);
        cube_leaves.transform.Translate(new Vector3(Random.Range(-1f, 1f), Random.Range(4f, 8f), Random.Range(-1f, 1f)));
        timer = 0;
    }
   
}
