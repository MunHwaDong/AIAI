using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using static TURN;
using static ACTION;

public class RenderingManager : MonoBehaviour
{

    [SerializeField]
    private BlackJackManager blackJackManager;

    [SerializeField]
    private UIManager UIManager;

    [SerializeField]
    private AIController ai;

    void Start()
    {
        
    }

    public void UISetting() // Start�� �ִ� �Լ����� �Űܿ��� ��ư�� ������ �����ϰ� �����ϱ� ���ؼ�
    {
        UIManager.UIinit();
        UIManager.BlackJack();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Card targetCard = hitInfo.transform.gameObject.GetComponent<Card>();

                if (targetCard.isTargetFlip && !targetCard.isRotating)
                {
                    Utill.ToggleCollider(hitInfo.transform.gameObject);
                    targetCard.GetComponent<Card>().Rotate();
                }
                else
                {
                    Utill.ToggleCollider(hitInfo.transform.gameObject);
                }
            }

        }
    }
}