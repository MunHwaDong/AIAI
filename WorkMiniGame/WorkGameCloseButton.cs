using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkGameCloseButton : MonoBehaviour
{
    public WorkUIManager W_UI_Manager;
    public GameObject Ingame_UI;
    public GameObject PartTime_Window;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseButton()
    {
        if (Ingame_UI.activeSelf == false)
        {
            Debug.Log(Ingame_UI.activeSelf);
            PartTime_Window.SetActive(false);
        }
        else
        {
            W_UI_Manager.PrintWarning();
        }
    }
}
