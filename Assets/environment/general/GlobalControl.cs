using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public float rain_factor { get; set; }
    public int wind_factor { get; set; }


    public Material skyMaterial;
    public Material groundMaterial_Output;
    public Material gourndMaterial_Input;
    public Material CloudMaterial;
    public Material FrondMaterial_Output;
    public Material FrondMaterial_Input;
    public float ExitTime;
    
    public float skyThickness;
    public Color FrondColor;
    private Color CloudColor;
    public GameObject treeToGrow; 
    public GameObject cloud;
    private List<GameObject> Clouds;
    public GameObject leafleaf;


    private float wind_duration;
    private float rain_duration;
    private int rain_times;
    public int num_of_clouds;

    // Start is called before the first frame update
    void Awake()
    {
        rain_factor = 0;
        rain_times = 0;
        //skyColor = skyMaterial.GetColor("_SkyTint"); //FF0120
        //GroundColor = groundMaterial.GetColor("_BaseColor"); //673664
        CloudColor = CloudMaterial.GetColor("_BaseColor");
        Clouds = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wind_factor > 50)
        {
            wind_duration = 15f;
            GetComponents<AudioSource>()[0].enabled = true;
            wind_factor = 0;
        }
        if (wind_duration > 0)
        {
            wind_duration -= Time.deltaTime;
            wind(); 
            if (wind_duration <= 0)
            {
                windStop();
            }
        }

        //  RenderSettings.skybox.SetColor("_SkyTint", skyColor);
        //skyMaterial.SetColor("_SkyTint", skyColor);
       
        float ccc = rain_factor * 0.1f;
        CloudMaterial.SetColor("_BaseColor", CloudColor - new Color(ccc, ccc, ccc, 0));
        //skyThickness = 1.5f - ccc;

        if (ExitTime < 1 && ExitTime > 0)
        {
            groundMaterial_Output.Lerp(groundMaterial_Output, gourndMaterial_Input, Time.deltaTime);
            FrondMaterial_Output.Lerp(FrondMaterial_Output, FrondMaterial_Input, Time.deltaTime);

            //RenderSettings.skybox.SetFloat("_AtmosphereThickness", skyThickness);
            //RenderSettings.skybox = skyMaterial;
        }


        if (rain_factor > 5)
        {
            addCloud();
            rain_factor = 0;
        }

        if (num_of_clouds > 40)
        {
            rain_duration = 25f;
            num_of_clouds = 0;
            rain_times++;
            gameObject.GetComponent<Animator>().SetTrigger("trigger_rain");
            transform.GetChild(0).gameObject.SetActive(true); //rain particle system
            GetComponents<AudioSource>()[1].Play();

            flower_grab[] flowers = FindObjectsOfType<flower_grab>();
            foreach (flower_grab f in flowers)
            {
                int opportunity = Random.Range(0, 45);
                Debug.Log(opportunity);
                if (opportunity == 3 && f.touchGround)
                {
                    Instantiate(treeToGrow, f.transform.position, Quaternion.identity);
                    Destroy(f.gameObject);
                }
                if(opportunity > 38)
                {
                    Instantiate(leafleaf, f.transform.position + new Vector3 (0,1,0), Quaternion.identity);
                    Destroy(f.gameObject);
                }
            }

        }
        if (rain_duration > 0)
        {
            rain_duration -= Time.deltaTime;
            rain();
            if (rain_duration < 0)
            {
                rainStop();
            }
        }
    }

    private Vector3 windDirection(float time)
    {
        float x = 2 * Mathf.Sin(Time.time/3) + Mathf.Sin(time) * 3;
        float y = 10 + Mathf.Sin(time * 0.4f);
        float z = -3 * Mathf.Sin(Time.time/3) + Mathf.Sin(time) * 2f ;
        // time - Time.deltaTime;
        return new Vector3(x *  Random.Range(-1,1), y, z* Random.Range(-1,0));
    }
    void wind()
    {
        leaf[] leaves = FindObjectsOfType<leaf>();
        foreach(leaf l in leaves)
        {
            l.GetComponent<ConstantForce>().enabled = true;  
            l.GetComponent<ConstantForce>().force = windDirection(wind_duration);
        }
    }

    void windStop()
    {
        leaf[] leaves = FindObjectsOfType<leaf>();
        foreach (leaf l in leaves)
        {
            l.GetComponent<ConstantForce>().enabled = false;
        }
        GetComponents<AudioSource>()[0].enabled = false;
    }
   
    void rain()
    {
        tree_grow[] trees = FindObjectsOfType<tree_grow>();
        foreach (tree_grow t in trees)
        {
            t.grow();
        }

        leaf[] leaves = FindObjectsOfType<leaf>();
        foreach (leaf l in leaves)
        {
            if (l.GetComponent<Rigidbody>().velocity.magnitude < 0.05f)
            {
                int opportunity = Random.Range(0, 100);
                if (opportunity == 3 && l.touchGround)
                {
                    Instantiate(treeToGrow, l.transform.position, Quaternion.identity);
                    Destroy(l.gameObject);
                }
            }
        }
        foreach (GameObject c in Clouds)
        {
            int opportunity = Random.Range(0, 5);
            if (opportunity == 3)
            {
                Destroy(c);
            }
        }
    }
    void rainStop()
    {
        transform.GetChild(0).gameObject.SetActive(false); //rain particle system
        GetComponent<Animator>().SetTrigger("trigger_rainStop");
        
    }

    void addCloud()
    {
        Vector3 pos = new Vector3(Random.Range(-96, 105), Random.Range(40,70), Random.Range(-160, 180));
        GameObject g = Instantiate(cloud, pos, Quaternion.identity);
        Vector3 rot = new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
        g.transform.Rotate(rot);
        g.transform.localScale *= Random.Range(4f, 10f);
        Clouds.Add(g);
        num_of_clouds++;
    }
}
