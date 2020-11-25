using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class teleport_trigger : MonoBehaviour
{
    public GameObject lightBeam;
    public GameObject[] grow_trees;
    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(" play Audio");
    //    if (other.CompareTag("Player")) //Player layer 9
    //    {

    //    }
    //}

    void OnHandHoverBegin(Hand hand)
    {
        Animation a = gameObject.GetComponent<Animation>();
        a.Play();      // GetClip("button_press")
        AudioSource[] au = gameObject.GetComponents<AudioSource>();
        au[0].Play();
        //au[1].Play();

        lightBeam.SetActive(true);
        foreach(GameObject tree in grow_trees)
        {
            tree.SetActive(true);
        }
    }
}
