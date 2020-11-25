using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_drag : MonoBehaviour
{
    private Vector3 offset;
    private float mZCoord;
    private Color originalColor;
    
    void OnMouseDown(){

        //Get the Renderer component from the new cube
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color;
        //Call SetColor using the shader property name "_Color" and setting the color to red
        cubeRenderer.material.SetColor("_Color", Color.red);

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - MouseWolrdPos();
   
    }
    private Vector3 MouseWolrdPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseUp()
    {
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", originalColor);
    }
    void OnMouseDrag(){
        transform.position = MouseWolrdPos() + offset;
        
    }
  
}
