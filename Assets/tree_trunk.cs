using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tree_trunk : MonoBehaviour
{

    public int nu_of_branches;
    public GameObject rock;
    private float start_time;

    void Start()
    {
        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - start_time > 3 && nu_of_branches < 1)
        {
            for(int i=0; i < 5; i++)
            {
                Vector3 pos = new Vector3(this.transform.position.x, 0, this.transform.position.z) + new Vector3(0, i, 0)/2;
                Instantiate(rock, pos, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree_Branch"))
        {
            nu_of_branches++;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree_Branch"))
        {
            nu_of_branches--;
        }
    }
  
    
}
