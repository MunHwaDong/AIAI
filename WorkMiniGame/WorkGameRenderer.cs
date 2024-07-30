using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkGameRenderer : MonoBehaviour
{

    [SerializeField]
    private WorkUIManager uiManager;

    void Update()
    {
        if(uiManager.GameOver())    
        {
            
            StartCoroutine(UpdateGameOver());
            this.gameObject.SetActive(false);
        }
        else
        {
            uiManager.UpdateTimer();
        }
    }

    private IEnumerator UpdateGameOver()
    {
        yield return StartCoroutine(uiManager.ShowGameOverUI());
        yield return null;
    }

}
