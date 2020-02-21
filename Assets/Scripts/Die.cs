using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{

    private Shader myShader;
    private Material myMaterial;
    private float value;
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        myShader = Shader.Find("Custom/DieShader");
        myMaterial = new Material(myShader);
        //Debug.Log(myShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (myShader != null)
        {
            Graphics.Blit(source, destination, myMaterial);
        }
        else 
        {
            Graphics.Blit(source, destination);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            value = Mathf.Lerp(value, 1, Time.deltaTime * 0.8f);
            myMaterial.SetFloat("_Value", value);
        }
    }

    //public void gameOverShader()
    //{
        
    //}

}
