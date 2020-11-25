using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class tree_branches : MonoBehaviour
{
    public GameObject treeToGrow;
    public float grow_size_time { get; set; }
   

    public Material MaterialWhenGrab;
    private int triggerEnterTree;
    private Interactable interactable;
   
    private Material material_Original;
    private Color color_Original;
  
    public GameObject leaf;
    private float d_p;
    private Vector3 Hand_Prev_Position;
    private int add_branch;

    public GameObject leaf_cube;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.highlightOnHover = false;
        material_Original = gameObject.GetComponent<Renderer>().material;
        color_Original = material_Original.GetColor("_BaseColor");
        add_branch = 0;
        d_p = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.lossyScale.z > 0.6)
        {
            //become telport area
            transform.GetChild(0).gameObject.SetActive(true);
            //become interactable
        }
        if (gameObject.transform.lossyScale.z <= 0.6)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        if (grow_size_time > 0)
        {
            growInSize();
            grow_size_time -= Time.deltaTime;
        }


        if (d_p > 0.5f)
        {
            d_p = 0;
            instantiateLeaf(1, gameObject.transform.position);
            gameObject.GetComponents<AudioSource>()[1].PlayDelayed(0); 
            add_branch++;
           // this.transform.localScale *= 1.2f;

        }
        if (add_branch > 4 && this.transform.localScale.y<1)
        {
            add_branch = 0;
            this.transform.localScale *= 1.2f;

            //GameObject g = Instantiate(this.gameObject, transform.position , Quaternion.identity);
            //g.transform.localScale = this.transform.lossyScale * 0.7f;
            //g.tag = "Tree_Branch";
            //g.GetComponent<Renderer>().material = material_Original;
            //g.transform.position += newBranch_Position() * (g.transform.lossyScale.y + gameObject.transform.lossyScale.y)* 0.6f;
        }
    }

    private Vector3 newBranch_Position()
    {
        Vector3 r = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        if( r == new Vector3(0, 0, 0))
        {
            return new Vector3(1, 1, 1);
        }
        else
        {
            return r;
        }
    }
    private void growInSize()
    {
        gameObject.transform.localScale *= 1.005f;
    }
    private void instantiateLeaf(int number, Vector3 pos)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject g = Instantiate(leaf, pos, Quaternion.identity);

            GlobalControl glob = FindObjectOfType<GlobalControl>();
            glob.wind_factor++;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        //if (col.gameObject.CompareTag("Terrain") && gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.1) 
        //{
        //    Debug.Log("branch collide with ground");

        //    gameObject.GetComponents<AudioSource>()[0].PlayDelayed(0);
        //    instantiateLeaf(5, gameObject.transform.position);
        //    this.transform.localScale *= 0.8f;
        //    if (this.transform.lossyScale.y < 0.2)
        //    {
        //        Destroy(gameObject);
        //    }           
        //}
        if (col.gameObject.CompareTag("rock")) //&& col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 2
        {
            gameObject.GetComponents<AudioSource>()[0].PlayDelayed(0);
            gameObject.GetComponent<Animator>().SetTrigger("shake");
            instantiateLeaf(7, col.gameObject.transform.position);

            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Tree_Branch")|| col.gameObject.CompareTag("tree"))
        {
            if (triggerEnterTree < 1)
            {
                triggerEnterTree = 0;
            }
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", color_Original);
            triggerEnterTree++;
        }

       
        if (col.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Tree_Branch") || col.gameObject.CompareTag("tree"))
        {
            triggerEnterTree--;
            if (triggerEnterTree < 1)
            {
                triggerEnterTree = 0;
                Color c = gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor");
                c.r += 0.4f;
                gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", c);

            }
        }

        
    }

    private void OnCollisionEnter(Collision col)
    {
       
    }


    private void OnHandHoverBegin(Hand hand)
    {
       
        gameObject.GetComponent<Animator>().enabled = false;
        // hand.ShowGrabHint();
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
        bool isGrabEnding = hand.IsGrabEnding(gameObject);
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
            d_p = 0;
            add_branch = 0;
            if (triggerEnterTree < 1)
            {
                GameObject g = Instantiate(leaf_cube, this.transform.position, Quaternion.identity);
                g.transform.localScale = this.transform.lossyScale;
                Destroy(gameObject);
            }
        }
        d_p += Vector3.Magnitude(hand.transform.position - Hand_Prev_Position);
        Hand_Prev_Position = hand.transform.position;
        this.transform.rotation = Quaternion.identity;
    }

    
}
