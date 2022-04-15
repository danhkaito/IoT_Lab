using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


public class ToggleController : MonoBehaviour, IPointerDownHandler
{
	public  bool isOn;
	public bool isOn2;
	public Color onColorBg;
	public Color offColorBg;
	private float curX;
	public Image toggleBgImage;

	// public GameObject handle;
	// private RectTransform handleTransform;

	public CanvasGroup toggle;
	private float handleSize;
	private float onPosX;
	private float offPosX;

	// public float handleOffset;

	// public GameObject onIcon;
	// public GameObject offIcon;
  	public GameObject switchBtn;


	public float speed;
	static float t = 0.0f;

	// private bool switching = false;





	void Start()
	{
		curX=Mathf.Abs(switchBtn.transform.localPosition.x);
    	isOn2=isOn=(switchBtn.transform.localPosition.x<0)?false:true;;
		if(isOn)
		{
			toggleBgImage.color = onColorBg;
			// handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
			// onIcon.gameObject.SetActive(true);
			// offIcon.gameObject.SetActive(false);
		}
		else
		{
			toggleBgImage.color = offColorBg;
			// handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
			// onIcon.gameObject.SetActive(false);
			// offIcon.gameObject.SetActive(true);
		}

	}
		
	void Update()
	{

		if(isOn!=isOn2)
		{
			toggle.interactable=false;
			switchBtn.transform.DOLocalMoveX(((isOn2?-1:1)*curX),0.2f).OnComplete(()=>{
				// toggle.blocksRaycasts = true;
				// toggle.interactable=true;
			});
			toggleBgImage.DOColor(isOn?onColorBg:offColorBg, 0.2f);
			isOn2=isOn;
		}
		// isOn2=isOn;
	}

	public void DoYourStaff()
	{
      	switchBtn.transform.DOLocalMoveX(((isOn2?-1:1)*curX),2f);
		toggleBgImage.DOColor(isOn?onColorBg:offColorBg, 2f);
	}

	public void Switching()
	{
		// switching = true;
		isOn=!isOn2;
	}
		
    public void OnPointerDown(PointerEventData eventData)
        {
            toggle.interactable = false;
            toggle.blocksRaycasts = false;
            // backgroundImg.color = waitingColor;

            // if (isOn)
            //     mqtt.PublishButtonData(buttonName, "OFF");
            // else
            //     mqtt.PublishButtonData(buttonName, "ON");

            //Toggle(!isOn);
        }


	// public void Toggle(bool toggleStatus)
	// {
	// 	if(!onIcon.active || !offIcon.active)
	// 	{
	// 		onIcon.SetActive(true);
	// 		offIcon.SetActive(true);
	// 	}
		
	// 	if(toggleStatus)
	// 	{
	// 		toggleBgImage.color = SmoothColor(onColorBg, offColorBg);
	// 		Transparency (onIcon, 1f, 0f);
	// 		Transparency (offIcon, 0f, 1f);
	// 		handleTransform.localPosition = SmoothMove(handle, onPosX, offPosX);
	// 	}
	// 	else 
	// 	{
	// 		toggleBgImage.color = SmoothColor(offColorBg, onColorBg);
	// 		Transparency (onIcon, 0f, 1f);
	// 		Transparency (offIcon, 1f, 0f);
	// 		handleTransform.localPosition = SmoothMove(handle, offPosX, onPosX);
	// 	}
			
	// }


	// Vector3 SmoothMove(GameObject toggleHandle, float startPosX, float endPosX)
	// {
		
	// 	Vector3 position = new Vector3 (Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);
	// 	StopSwitching();
	// 	return position;
	// }

	// Color SmoothColor(Color startCol, Color endCol)
	// {
	// 	Color resultCol;
	// 	resultCol = Color.Lerp(startCol, endCol, t += speed * Time.deltaTime);
	// 	return resultCol;
	// }

	// CanvasGroup Transparency (GameObject alphaObj, float startAlpha, float endAlpha)
	// {
	// 	CanvasGroup alphaVal;
	// 	alphaVal = alphaObj.gameObject.GetComponent<CanvasGroup>();
	// 	alphaVal.alpha = Mathf.Lerp(startAlpha, endAlpha, t += speed * Time.deltaTime);
	// 	return alphaVal;
	// }

	// void StopSwitching()
	// {
	// 	if(t > 1.0f)
	// 	{
	// 		switching = false;
	// 		t = 0.0f;
	// 		switch(isOn2)
	// 		{
	// 		case true:
	// 			isOn2 = false;
	// 			DoYourStaff();
	// 			break;

	// 		case false:
	// 			isOn2 = true;
	// 			DoYourStaff();
	// 			break;
	// 		}
	// 		// isOn2=isOn;

	// 	}

	// }

}
