using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon_Hovering : MonoBehaviour
{
    public Sprite Default_Image;
    public Sprite Hovering_Image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Image_Hovering()
    {
        gameObject.GetComponent<Image>().sprite = Hovering_Image;    
    }
    public void Image_Default()
    {
        gameObject.GetComponent<Image>().sprite = Default_Image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
