using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaf : MonoBehaviour
{
    public float windDuration { get; set; }
    public bool touchGround;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position += new Vector3(Random.Range(-0.5f,0.5f),0,Random.Range(-0.5f,0.5f));
        this.GetComponent<ConstantForce>().relativeForce = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
        touchGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (windDuration > 0)
        //{
        //    wind();
        //    windDuration -= Time.deltaTime;
        //}
        
    }

    void wind()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("out_Terrain"))
        {
            touchGround = true;
        }
        if (other.CompareTag("tornado"))
        {
            gameObject.GetComponent<ConstantForce>().enabled = true;
            gameObject.GetComponent<ConstantForce>().force = (other.transform.position - this.transform.position) * 0.5f + new Vector3(0, 2, 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("out_Terrain"))
        {
            touchGround = false;
        }
        if (other.CompareTag("tornado"))
        {
            gameObject.GetComponent<ConstantForce>().enabled = false;
        }
    }

}
