using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(-Input.GetAxis("Vertical"), 0, 0);
        this.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }
}
