using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class rock_smash : MonoBehaviour
{
    private Interactable interactable;
    Vector3 velocity;
    public SteamVR_Action_Vector2 input;
    private Vector2 prev_input;
    public bool isGrabing { get; set; }  //set from <Throwable>
    public GameObject leaf;
    public GameObject flame;
    public Material RockOutMateiral;
    public PhysicMaterial bouncyMateiral;
    

    private bool isOutside;
    private int IsClockwise(Vector2 first, Vector2 second)
    {
        if (first == second)
        {
            return 0;
        }

        float angle1 = Mathf.Atan2(first.x, first.y);
        float angle2 = Mathf.Atan2(second.x, second.y);

        if ( angle1 - angle2 > 0 && angle1 - angle2 < Mathf.PI)
        {
            return 1;
        }

        if ( angle1-angle2 <0 || angle1 - angle2 > Mathf.PI)
        {
            return -1;
        }

        else
        {
            return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        isOutside = false;
    }

    void Update()
    {     
        velocity = gameObject.GetComponent<Rigidbody>().velocity;
        if (isGrabing && input != null)
        {
            resize(input.axis);
        }

        if (velocity.y > 8)
        {
            gameObject.AddComponent<ConstantForce>();
            ConstantForce cf = gameObject.GetComponent<ConstantForce>();
            cf.force = new Vector3 (velocity.x/5,10,velocity.y/5);
            if(velocity.y > 15)
            {
                fireworkEffect();
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Terrain") || col.gameObject.CompareTag("rock_collide") || col.gameObject.CompareTag("tree"))
        {
            GetComponent<AudioSource>().Play(0);

            if (velocity.magnitude > 5)
            {
                GetComponent<AudioSource>().Play(1);
                gameObject.transform.localScale *= 0.5f;
                for (int i = 0; i < 3; i++)
                {
                    GameObject g = Instantiate(gameObject, gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
                }
                // Destroy(gameObject);
            }
        }
    
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("wall"))
        {
            GetComponent<AudioSource>().Play(2);
            Debug.Log("collide with wall");
            isOutside = true;
            gameObject.GetComponent<MeshRenderer>().material = RockOutMateiral;
            gameObject.GetComponent<MeshCollider>().material = bouncyMateiral;
        }
        if (col.gameObject.CompareTag("flower"))
        {
            col.gameObject.transform.localScale *= Random.Range(0.8f, 1.2f);
        }
    }

    public void resize(Vector2 touchPad)
    {
       //Debug.Log(input.axis.x + "    " + input.axis.y);
        float a = (touchPad - prev_input).magnitude;
        int b = IsClockwise(prev_input, touchPad);
        gameObject.transform.localScale *= 1 + a * b * 0.2f;

        prev_input = touchPad;
    }
    private void fireworkEffect()
    {
        for (int i=0; i <9; i++)
        {
            Instantiate(flame, this.transform.position, Quaternion.identity);
            
        }
        Destroy(gameObject);
    }
}
