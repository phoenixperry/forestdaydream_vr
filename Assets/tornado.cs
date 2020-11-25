using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
    public GameObject leaf;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Tree_Branch"))
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(leaf, col.transform.position, Quaternion.identity);
            }
            Destroy(col);
        }
    }
}
