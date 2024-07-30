using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cap_Deco : MonoBehaviour
{
    public RectTransform transform_hour;
    public RectTransform transform_min;
    public RectTransform transform_sec;
    public Text text_date;
    public Text text_get;
    public Button button_get;

    float angle = 80;
    private float lerpTime = 0;
    private float speed = 5f;
    public Rigidbody2D rigidbody;
    bool direction = true;

    private void OnEnable()
    {
        angle = 80;
        StartCoroutine(Pendulum());
    }

    public void Re_Pendulum()
    {
        angle = 80;
        StartCoroutine(Pendulum());
    }
    IEnumerator Pendulum()
    {
        float time = 0.0f;
        while(angle > 0)
        {
            transform.rotation = CalculateMovementOfPendulum();
            angle = 80 - (time * 80);
            time += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        angle = 0;
        yield return null;
    }
    private void Update()
    {
        lerpTime += Time.deltaTime * speed;
        
    }

    Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
            Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime) + 1) * 0.5f;
    }


    // Update is called once per frame
    
}
