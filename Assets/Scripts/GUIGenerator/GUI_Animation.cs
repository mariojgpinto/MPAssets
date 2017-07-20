using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class GUI_Animation : MonoBehaviour {
	#region ANIMANTIONS
	public enum ANIMANTION{
		SHOW_HIDE = 0,
		SCALE = 1
	}
	#endregion
	
	#region VARIABLES
	public static GUI_Animation instance;
	#endregion

	#region SWITCH_ANIMATION
	public static void SwitchMenus(GameObject menuFrom, GameObject menuTo, ANIMANTION animation = ANIMANTION.SHOW_HIDE) {
		switch (animation) {
			case ANIMANTION.SCALE:
				GUI_Animation.ScaleDown(menuFrom);
				GUI_Animation.ScaleUp(menuTo);
				break;
			case ANIMANTION.SHOW_HIDE:
			default:
				GUI_Animation.HideMenu(menuFrom);
				GUI_Animation.ShowMenu(menuTo);
				break;
		}
	}
	#endregion

	#region SHOW_HIDE
	public static void ShowMenu(GameObject panel)
	{
		panel.SetActive(true);
		panel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
	}

	public static void HideMenu(GameObject panel)
	{
		panel.SetActive(false);
		panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(panel.GetComponent<RectTransform>().rect.width,0,0);
	}
	#endregion

	#region SCALE
	public static void ScaleDown(GameObject menu) {
		instance.StartCoroutine(Scale_routine(menu, Vector3.one, Vector3.zero));
	}

	public static void ScaleUp(GameObject menu) {
		instance.StartCoroutine(Scale_routine(menu, Vector3.zero, Vector3.one));
	}

	public static IEnumerator Scale_routine(GameObject panel, Vector3 scaleInit, Vector3 scaleEnd, float animationTime = .25f, float delayTime = 0f, bool removeElement = false) {
		float progress = 0;

		if (delayTime > 0)
			yield return new WaitForSeconds(delayTime);

		if (!removeElement) {
			panel.SetActive(true);
		}

		RectTransform panelPos = panel.GetComponent<RectTransform>();

		Vector3 deltaScale = scaleEnd - scaleInit;
		Vector3 endScale = scaleEnd;

		while (progress < 1) {
			panelPos.localScale = scaleInit + deltaScale * progress;

			progress += Time.deltaTime / animationTime;
			yield return true;
		}

		panelPos.localScale = endScale;

		if (removeElement) {
			panel.SetActive(false);
		}

		yield break;
	}
	#endregion

	#region SLIDE
	public static IEnumerator SlidePanel_Routine(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float animationTime = .25f, float delayTime = 0f, bool removeElement = false)
	{
		float progress = 0;

		if(delayTime>0)
			yield return new WaitForSeconds(delayTime);

		if(!removeElement){
			panel.SetActive(true);
		}

		RectTransform panelPos = panel.GetComponent<RectTransform>();

		Vector3 deltaPosition = finalPosition - initialPosition;
		Vector3 endPosition = finalPosition;

		while(progress < 1)
		{
			panelPos.anchoredPosition = initialPosition + deltaPosition * progress;

			progress += Time.deltaTime/animationTime;
			yield return true;
		}

		panelPos.anchoredPosition = endPosition;

		if(removeElement){
			panel.SetActive(false);
		}

		yield break;
	}

	public static void SlidePanel(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float animationTime = .25f, float delayTime = 0f, bool removeElement = false)
	{
		instance.SlidePanel_Instance(panel, initialPosition, finalPosition, animationTime, delayTime, removeElement);
	}

	public static void BringBackFromRight(GameObject panel, float animationTime = .25f, float delayTime = 0f)
	{
		RectTransform rect = panel.GetComponent<RectTransform>();
		if(rect.offsetMin.x == rect.offsetMax.x){
			instance.SlidePanel_Instance(panel, new Vector3(rect.rect.width,0,0), Vector3.zero, animationTime, delayTime);
		}
		else{
			instance.SlidePanel_Instance(panel, new Vector3(rect.rect.width / 2f,0,0), new Vector3(-rect.rect.width / 2f,0,0), animationTime, delayTime);
		}
	}

	public static void BringBackFromLeft(GameObject panel, float animationTime = .25f, float delayTime = 0f)
	{
		RectTransform rect = panel.GetComponent<RectTransform>();
		if(rect.offsetMin.x == rect.offsetMax.x){
			instance.SlidePanel_Instance(panel, new Vector3(-rect.rect.width,0,0), Vector3.zero, animationTime, delayTime);
		}
		else{
			instance.SlidePanel_Instance(panel, new Vector3(-rect.rect.width / 2f,0,0), new Vector3(rect.rect.width / 2f,0,0), animationTime, delayTime);
		}
	}

	public static void RemoveToRight(GameObject panel, float animationTime = .25f, float delayTime = 0f)
	{
		RectTransform rect = panel.GetComponent<RectTransform>();
		if(rect.offsetMin.x == rect.offsetMax.x){
			instance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(rect.rect.width,0,0), animationTime, delayTime);
		}
		else{
			instance.SlidePanel_Instance(panel, new Vector3(-rect.rect.width / 2f,0,0),new Vector3(rect.rect.width / 2f,0,0), animationTime, delayTime);
		}
	}

	public static void RemoveToLeft(GameObject panel, float animationTime = .25f, float delayTime = 0f)
	{
		RectTransform rect = panel.GetComponent<RectTransform>();
		if(rect.offsetMin.x == rect.offsetMax.x){
			instance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(-rect.rect.width,0,0), animationTime, delayTime);
		}
		else{
			instance.SlidePanel_Instance(panel, new Vector3(rect.rect.width / 2f,0,0),new Vector3(-rect.rect.width / 2f,0,0), animationTime, delayTime);
		}
	}

	public static void BringBackFromTop(GameObject panel, float animationTime = .25f, float delayTime = 0f)
	{
		instance.SlidePanel_Instance(panel, new Vector3(0,panel.GetComponent<RectTransform>().rect.height,0), Vector3.zero, animationTime, delayTime);
	}

	public static void BringBackFromBottom(GameObject panel, float animationTime = .25f, float delayTime = 0f)
	{
		instance.SlidePanel_Instance(panel, new Vector3(0,-panel.GetComponent<RectTransform>().rect.height,0), Vector3.zero, animationTime, delayTime);
	}

	public static void RemoveToTop(GameObject panel, float animationTime = .25f, float delayTime = 0f)
	{
		instance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(0,panel.GetComponent<RectTransform>().rect.height,0), animationTime, delayTime, true);
	}

	public static void RemoveToBottom(GameObject panel, float animationTime = .25f, float delayTime = 0f)
	{
		instance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(0,-panel.GetComponent<RectTransform>().rect.height,0), animationTime, delayTime, true);
	}

	void SlidePanel_Instance(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float animationTime = .25f, float delayTime = 0f, bool removeElement = false)
	{
		StartCoroutine(SlidePanel_Routine(panel, initialPosition, finalPosition, animationTime, delayTime, removeElement));
	}

	#endregion

	#region UNITY_CALLBACKS
	void Awake()
	{
		GUI_Animation.instance = this;
	}
	#endregion
}
