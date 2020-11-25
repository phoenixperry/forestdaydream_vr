using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class grow_grass : MonoBehaviour
{

    private float timer;
    private float timer2;
    public GameObject grass1;
    public GameObject grass2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();

    }

    //-------------------------------------------------
    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }

    private void HandHoverUpdate(Hand hand)
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            grassToGrow();
            timer = 0;
        }
        if (timer2 > 3)
        {
            grassToGrow2();
            timer2 = 0;
        }
    }
    void grassToGrow()
    {
        GameObject go = Instantiate(grass1, this.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);

        go.transform.Translate(new Vector3(Random.Range(-9f, 9f), 0, Random.Range(-9f, 9f)));
        go.transform.Rotate(0, Random.Range(0, 360), 0);

    }
    void grassToGrow2()
    {
        GameObject go = Instantiate(grass2, this.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);

        go.transform.Translate(new Vector3(Random.Range(-9f, 9f), 0, Random.Range(-9f, 9f)));
        go.transform.Rotate(0, Random.Range(0, 360), 0);
    }
}
