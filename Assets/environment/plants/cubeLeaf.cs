using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeLeaf : MonoBehaviour
{
    public GameObject leaf;
    public GameObject tree_Branch;
    public GameObject treeToGrow;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.y > 3)
        {
            gameObject.GetComponents<AudioSource>()[0].PlayDelayed(0);
            for (int i = 0; i < 5; i++)
            {
                GameObject g = Instantiate(leaf, this.transform.position, Quaternion.identity);

                GlobalControl glob = FindObjectOfType<GlobalControl>();
                glob.wind_factor++;
            }
            this.transform.localScale *= 0.9f;
            if (this.transform.lossyScale.y < 0.1)
            {
                Destroy(gameObject);
            }
        }
        if (col.gameObject.CompareTag("Terrain") )
        {
            gameObject.GetComponents<AudioSource>()[0].PlayDelayed(0);

            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
            {
                Debug.Log("branch collide with ground");
                if (this.transform.lossyScale.y > 0.5f)
                {
                    Instantiate(treeToGrow, col.contacts[0].point, Quaternion.identity);
                    Destroy(gameObject);
                }
                for (int i = 0; i < 5; i++)
                {
                    GameObject g = Instantiate(leaf, this.transform.position, Quaternion.identity);

                    GlobalControl glob = FindObjectOfType<GlobalControl>();
                    glob.wind_factor++;
                }
                this.transform.localScale *= 0.9f;
                if (this.transform.lossyScale.y < 0.1)
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("tree") || col.gameObject.CompareTag("Tree_Branch"))
        {
            GameObject g = Instantiate(tree_Branch, this.transform.position, Quaternion.identity);
            g.transform.localScale = this.transform.localScale;
            Destroy(gameObject);
            
        }
        if (col.gameObject.CompareTag("flower"))
        {
            Destroy(col.gameObject);
            if (gameObject.transform.lossyScale.y < 0.4)
            {
                gameObject.transform.localScale *= 1.05f;
            } 
        }
        if (col.gameObject.CompareTag("fruit"))
        {
            Instantiate(treeToGrow, this.transform.position - new Vector3 (0,this.transform.lossyScale.y,0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}