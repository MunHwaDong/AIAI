using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
	[SerializeField]
	private Ease easeType = Ease.OutBack;

	[SerializeField]
	private float wantedScale = 1f;

	[SerializeField]
	private float animationTime = 1.5f;

	public void Init()
	{
		transform.localScale = Vector3.zero;
	}

	public void Animate()
	{
		transform.DOScale(wantedScale, animationTime).SetEase(easeType);
	}
}
