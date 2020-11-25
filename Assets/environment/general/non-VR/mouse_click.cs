using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_click : MonoBehaviour
{
    private Color originalColor;
    private int click;
    void OnMouseDown()
    {
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color;
        cubeRenderer.material.SetColor("_Color", Color.red);
        click++;
        if (click == 1)
        {
            Debug.Log("click");
        }
        
    }
  
    private void OnMouseUp()
    {
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", originalColor);
        click = 0;
    }
}
