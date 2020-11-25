using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class fruit : MonoBehaviour
{
    public GameObject[] flowers;
    
    public bool isGrabing { get; set; }
    public int numbering { get; set; }
    private int timesOfCollision;
    // Start is called before the first frame update
    void Start()
    {
        timesOfCollision = 0;
        AudioSource a = gameObject.GetComponents<AudioSource>()[0];
        a.pitch = Random.Range(0.7f, 1.6f);
        a.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabing)
        {
            
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Terrain") || col.gameObject.CompareTag("out_Terrain"))
        {
            if (timesOfCollision < 10)
            {
                AudioSource a = gameObject.GetComponents<AudioSource>()[1];
                a.pitch = Random.Range(0.7f, 1.6f);
                a.Play();
                GameObject g = Instantiate(flowers[numbering], col.contacts[0].point - new Vector3(0, 0.1f, 0), Quaternion.identity);
                g.GetComponent<Animator>().SetTrigger("scale");
                gameObject.GetComponent<Rigidbody>().velocity += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                Debug.Log("fruit collide");
                timesOfCollision++;
                GlobalControl glob = FindObjectOfType<GlobalControl>();
                glob.rain_factor++;
            }
        }
    }

    public void ChangeToNextFlower() //triggerd by <Throwable>
    {
        timesOfCollision = 0;
        numbering++;
        if (numbering > 2)
        {
            numbering -= 3;
        }
    }  
}
