using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAni : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(leftPopupAnimation());
    }
    private void OnDisable()
    {
        RectTransform thisRect = (RectTransform)gameObject.transform;
        thisRect.sizeDelta = new Vector2(0, 0);
    }

    IEnumerator leftPopupAnimation()
    {
        RectTransform thisRect = (RectTransform)gameObject.transform;
        thisRect.sizeDelta = new Vector2(0, 0);
        float time = 0.0f;
        while (time < 0.2)
        {
            thisRect.sizeDelta = new Vector2(525 * (time * 10), 310 * (time * 10));
            time = time + 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        thisRect.sizeDelta = new Vector2(1050, 620);
        yield return null;
    }
}
