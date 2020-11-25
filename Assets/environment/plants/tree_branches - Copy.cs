//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Valve.VR;
//using Valve.VR.InteractionSystem;

//public class tree_branches : MonoBehaviour
//{
//    public GameObject treeToGrow;
//    public float grow_size_time { get; set; }
   

//    public Material MaterialWhenGrab;
//    private Interactable interactable;
//    private Vector3 handPos;
//    private Vector3 handTranslate;
//    private Material material_Original;
//    private Color color_Original;
  
//    public GameObject leaf;
//    private float d_p;
//    private Vector3 Hand_Prev_Position;
//    private int add_branch;


//    // Start is called before the first frame update
//    void Start()
//    {
//        interactable = GetComponent<Interactable>();
//        interactable.highlightOnHover = false;
//        material_Original = gameObject.GetComponent<Renderer>().material;
//        add_branch = 0;
//        d_p = 0;
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(gameObject.transform.lossyScale.z > 0.6)
//        {
//            //become telport area
//            transform.GetChild(0).gameObject.SetActive(true);
//            //become interactable
//        }
//        if (gameObject.transform.lossyScale.z <= 0.6)
//        {
//            transform.GetChild(0).gameObject.SetActive(false);
//        }


//        if (grow_size_time > 0 )
//        {
//            growInSize();
//            grow_size_time -= Time.deltaTime;
//        }

//        if (d_p > 0.5f)
//        {
//            d_p = 0;
//            Instantiate(leaf, this.transform.position, Quaternion.identity);
//            gameObject.GetComponents<AudioSource>()[1].PlayDelayed(0);
//            GlobalControl glob = FindObjectOfType<GlobalControl>();
//            glob.wind_factor++;
//            add_branch++;
//        }
//        if (add_branch > 4)
//        {
//            add_branch = 0;
//          //  this.transform.localScale *= 0.8f;
//            GameObject g = Instantiate(this.gameObject, transform.position , Quaternion.identity);
//            //g.transform.localScale = new Vector3 (0.15f,0.15f,0.15f);
//            g.tag = "Tree_Branch";
//            g.GetComponent<Renderer>().material = material_Original;
//            g.transform.position += newBranch_Position() * (g.transform.lossyScale.y + gameObject.transform.lossyScale.y)* 0.6f;
//        }
//    }

//    private Vector3 newBranch_Position()
//    {
//        Vector3 r = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
//        if( r == new Vector3(0, 0, 0))
//        {
//            return new Vector3(1, 1, 1);
//        }
//        else
//        {
//            return r;
//        }
//    }
    

//    public void growInSize()
//    {
//        gameObject.transform.localScale *= 1.005f;
//    }

//    public void growMoreBranches()
//    {
//        // this.posiiion, grab.position => new position
//        Debug.Log("grow more branches");
//    }

//    void OnTriggerEnter(Collider col)
//    {
//        if (col.gameObject.CompareTag("Terrain") && gameObject.transform.lossyScale.y > 0.08f) 
//        {
//            Debug.Log("branch collide with ground");      
//            GameObject g = Instantiate(treeToGrow, this.transform.position + new Vector3(0, -this.transform.lossyScale.y *1.2f, 0) , Quaternion.identity);
           
//            Destroy(gameObject);   
//        }

       
//        if (col.gameObject.CompareTag("Grabbing"))
//        {
//            Debug.Log(gameObject.transform.localScale);
//            gameObject.transform.localScale += col.gameObject.transform.localScale* 0.6f;
//            Debug.Log(col.gameObject.transform.localScale);
//            Debug.Log(gameObject.transform.localScale);
//            gameObject.GetComponents<AudioSource>()[2].PlayDelayed(0); //Play(2);
//            Destroy(col.gameObject);
//        }
//        if (col.gameObject.CompareTag("wall"))
//        {
//            Destroy(gameObject);
//        }

//    }

//    private void OnCollisionEnter(Collision col)
//    {
//        if (col.gameObject.CompareTag("rock")) //&& col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 2
//        {
//            gameObject.GetComponents<AudioSource>()[0].PlayDelayed(0);
//            gameObject.GetComponent<Animator>().SetTrigger("shake");
//            for (int i = 0; i < 7; i++)
//            {
//                GameObject g = Instantiate(leaf, col.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);

//                GlobalControl glob = FindObjectOfType<GlobalControl>();
//                glob.wind_factor++;
//            }

//            Destroy(col.gameObject);
//        }
//    }


//    private void OnHandHoverBegin(Hand hand)
//    {
       
//        gameObject.GetComponent<Animator>().enabled = false;
//        // hand.ShowGrabHint();
//        hand.HideGrabHint();
//        gameObject.GetComponent<Renderer>().material = MaterialWhenGrab;   // .SetColor("_BaseColor", a);
//    }
//    private void OnHandHoverEnd(Hand hand)
//    {
//        gameObject.GetComponent<Animator>().enabled = true;
//        //  hand.HideGrabHint();
//        gameObject.GetComponent<Renderer>().material = material_Original;

//    }
//    private void HandHoverUpdate(Hand hand)
//    {
//        GrabTypes grabType = hand.GetGrabStarting();
//        bool isGrabEnding = hand.IsGrabEnding(gameObject);
//        //grab the object
//        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
//        {
//            Hand.AttachmentFlags A = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

//            hand.AttachObject(gameObject, grabType, A);
//            hand.HoverLock(interactable);
//            gameObject.tag = "Grabbing";
//            Debug.Log(gameObject.tag+"   change tag");
//        }
        
//        //Release
//        else if (isGrabEnding)
//        {
//            hand.DetachObject(gameObject);
//            hand.HoverUnlock(interactable);
//            gameObject.tag = "Tree_Branch";
//            d_p = 0;
//            add_branch = 0;
//        }
//        d_p += Vector3.Magnitude(hand.transform.position - Hand_Prev_Position);
//        Hand_Prev_Position = hand.transform.position;
//        this.transform.rotation = Quaternion.identity;
//    }

    
//}
