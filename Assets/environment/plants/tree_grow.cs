using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_grow : MonoBehaviour
{
    public GameObject prefab_branch;
    public bool begin;
    


    private float start_grow_time;
    // Start is called before the first frame update
    void Start()
    {
        begin = false;
        start_grow_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (begin)
        {
            begin = false;
            Beginning();
        }
    }
    public void Beginning()
    {
        Vector3[] pos = { new Vector3(0.1f, 1.3f, -0.1f), new Vector3(0, 0.95f, 0), new Vector3(0.2f,0.73f,0.1f) };
   
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos1 = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 1.3f), Random.Range(-0.1f, 0.1f));
            GameObject g = Instantiate(prefab_branch, this.transform.position + pos1 ,Quaternion.identity, transform);
            //g.transform.GetComponent<Animator>().SetTrigger("scale");
            g.GetComponent<tree_branches>().grow_size_time = 1;
        }
        begin = false;
    }
    public void grow()
    {
        if (Time.time -start_grow_time > 3)
        {
            this.transform.localScale += new Vector3(1,1,1)* Random.Range(0.00004f, 0.00011f);
        }
    }
}
