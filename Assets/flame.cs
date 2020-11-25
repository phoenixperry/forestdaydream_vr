using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flame : MonoBehaviour
{
    public GameObject fruit;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1.8f);
    }

    void Update()
    {
        if (gameObject.transform.position.y < 3)
        {
            GameObject g = Instantiate(fruit, this.transform.position, Quaternion.identity);
            g.GetComponent<fruit>().numbering = Random.Range(0, 3);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Terrain"))
        {
            GameObject g = Instantiate(fruit, this.transform.position, Quaternion.identity);
            g.GetComponent<fruit>().numbering = Random.Range(0, 3);
            Destroy(gameObject);
        }
    }
}
