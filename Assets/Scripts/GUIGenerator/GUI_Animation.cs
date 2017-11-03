using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using MPAssets;

public class GUI_Animation : Singleton<GUI_Animation> {
	#region ANIMANTIONS
	public enum ANIMANTION {
		SHOW_HIDE = 0,
		SCALE = 1,
		FADE
	}

	public static float animationTime = .5f;
	#endregion

	#region SWITCH_ANIMATION
	public static void SwitchMenus(List<GameObject> menuFrom, List<GameObject> menuTo, ANIMANTION animation = ANIMANTION.SHOW_HIDE, float _animationTime = -1, float _delayTime = 0f) {
		switch (animation) {
			case ANIMANTION.SCALE:
				for (int i = 0; i < menuFrom.Count; ++i)
					GUI_Animation.ScaleDown(menuFrom[i], _animationTime, _delayTime);
				for (int i = 0; i < menuTo.Count; ++i)
					GUI_Animation.ScaleUp(menuTo[i], _animationTime, _delayTime);
				break;
			case ANIMANTION.FADE:
				for (int i = 0; i < menuFrom.Count; ++i)
					GUI_Animation.FadeOut(menuFrom[i], _animationTime, _delayTime);
				for (int i = 0; i < menuTo.Count; ++i)
					GUI_Animation.FadeIn(menuTo[i], _animationTime, _delayTime);
				break;
			case ANIMANTION.SHOW_HIDE:
			default:
				for (int i = 0; i < menuFrom.Count; ++i)
					GUI_Animation.HideMenu(menuFrom[i]);
				for (int i = 0; i < menuTo.Count; ++i)
					GUI_Animation.ShowMenu(menuTo[i]);
				break;
		}
	}

	public static void SwitchMenus(GameObject menuFrom, GameObject menuTo, ANIMANTION animation = ANIMANTION.SHOW_HIDE, float _animationTime = -1, float _delayTime = 0f) {
		switch (animation) {
			case ANIMANTION.SCALE:
				GUI_Animation.ScaleDown(menuFrom, _animationTime, _delayTime);
				GUI_Animation.ScaleUp(menuTo, _animationTime, _delayTime);
				break;
			case ANIMANTION.FADE:
				GUI_Animation.FadeOut(menuFrom, _animationTime, _delayTime);
				GUI_Animation.FadeIn(menuTo, _animationTime, _delayTime);
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
	public static void ShowMenu(GameObject panel) {
		panel.SetActive(true);
	}

	public static void HideMenu(GameObject panel) {
		panel.SetActive(false);
	}
	#endregion

	#region SCALE
	public static void ScaleDown(GameObject menu, float _animationTime = -1, float delayTime = 0f) {
		instance.StartCoroutine(Scale_routine(menu, Vector3.one, Vector3.zero, _animationTime, delayTime, true));
	}

	public static void ScaleUp(GameObject menu, float _animationTime = -1, float delayTime = 0f) {
		instance.StartCoroutine(Scale_routine(menu, Vector3.zero, Vector3.one, _animationTime, delayTime, false));
	}

	public static IEnumerator Scale_routine(GameObject panel, Vector3 scaleInit, Vector3 scaleEnd, float _animationTime = -1, float delayTime = 0f, bool removeElement = false) {
		float progress = 0;
		float animTime = _animationTime < 0 ? GUI_Animation.animationTime : _animationTime;

		if (delayTime > 0)
			yield return new WaitForSeconds(delayTime);

		panel.SetActive(true);

		RectTransform panelPos = panel.GetComponent<RectTransform>();
		panelPos.localScale = scaleInit;

		Vector3 deltaScale = scaleEnd - scaleInit;
		Vector3 endScale = scaleEnd;

		while (progress < 1) {
			panelPos.localScale = scaleInit + deltaScale * progress;

			progress += Time.deltaTime / animTime;
			yield return true;
		}

		panelPos.localScale = endScale;

		if (removeElement) {
			panel.SetActive(false);
		}

		yield break;
	}
	#endregion

	#region FADE
	public static void FadeIn(GameObject menu, float _animationTime = -1, float delayTime = 0f) {
		instance.StartCoroutine(Fade_routine(menu, 0, 1, _animationTime, delayTime, false));
	}

	public static void FadeOut(GameObject menu, float _animationTime = -1, float delayTime = 0f) {
		instance.StartCoroutine(Fade_routine(menu, 1, 0, _animationTime, delayTime, true));
	}

	public static IEnumerator Fade_routine(GameObject panel, float fadeFrom, float fadeTo, float _animationTime = -1, float delayTime = 0f, bool removeElement = false) {
		float progress = 0;
		float animTime = _animationTime < 0 ? GUI_Animation.animationTime : _animationTime;

		if (delayTime > 0)
			yield return new WaitForSeconds(delayTime);

		panel.SetActive(true);

		CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
		if (canvasGroup == null)
			canvasGroup = panel.AddComponent<CanvasGroup>();

		canvasGroup.alpha = fadeFrom;

		while (progress < 1) {
			canvasGroup.alpha = Mathf.Lerp(fadeFrom, fadeTo, progress);

			progress += Time.deltaTime / animTime;
			yield return true;
		}

		canvasGroup.alpha = fadeTo;

		if (removeElement) {
			panel.SetActive(false);
		}

		yield break;
	}
	#endregion

	#region SLIDE
	public static IEnumerator SlidePanel_Routine(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float _animationTime = -1, float delayTime = 0f, bool removeElement = false) {
		float progress = 0;
		float animTime = _animationTime < 0 ? GUI_Animation.animationTime : _animationTime;

		if (delayTime > 0)
			yield return new WaitForSeconds(delayTime);

		if (!removeElement) {
			panel.SetActive(true);
		}

		RectTransform panelPos = panel.GetComponent<RectTransform>();

		Vector3 deltaPosition = finalPosition - initialPosition;
		Vector3 endPosition = finalPosition;

		while (progress < 1) {
			panelPos.anchoredPosition = initialPosition + deltaPosition * progress;

			progress += Time.deltaTime / animTime;
			yield return true;
		}

		panelPos.anchoredPosition = endPosition;

		if (removeElement) {
			panel.SetActive(false);
		}

		yield break;
	}

	public static void SlidePanel(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float animationTime = -1, float delayTime = 0f, bool removeElement = false) {
		instance.SlidePanel_Instance(panel, initialPosition, finalPosition, animationTime, delayTime, removeElement);
	}

	public static void BringBackFromRight(GameObject panel, float animationTime = -1, float delayTime = 0f) {
		RectTransform rect = panel.GetComponent<RectTransform>();
		if (rect.offsetMin.x == rect.offsetMax.x) {
			instance.SlidePanel_Instance(panel, new Vector3(rect.rect.width, 0, 0), Vector3.zero, animationTime, delayTime);
		}
		else {
			instance.SlidePanel_Instance(panel, new Vector3(rect.rect.width / 2f, 0, 0), new Vector3(-rect.rect.width / 2f, 0, 0), animationTime, delayTime);
		}
	}

	public static void BringBackFromLeft(GameObject panel, float animationTime = -1, float delayTime = 0f) {
		RectTransform rect = panel.GetComponent<RectTransform>();
		if (rect.offsetMin.x == rect.offsetMax.x) {
			instance.SlidePanel_Instance(panel, new Vector3(-rect.rect.width, 0, 0), Vector3.zero, animationTime, delayTime);
		}
		else {
			instance.SlidePanel_Instance(panel, new Vector3(-rect.rect.width / 2f, 0, 0), new Vector3(rect.rect.width / 2f, 0, 0), animationTime, delayTime);
		}
	}

	public static void RemoveToRight(GameObject panel, float animationTime = -1, float delayTime = 0f) {
		RectTransform rect = panel.GetComponent<RectTransform>();
		if (rect.offsetMin.x == rect.offsetMax.x) {
			instance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(rect.rect.width, 0, 0), animationTime, delayTime);
		}
		else {
			instance.SlidePanel_Instance(panel, new Vector3(-rect.rect.width / 2f, 0, 0), new Vector3(rect.rect.width / 2f, 0, 0), animationTime, delayTime);
		}
	}

	public static void RemoveToLeft(GameObject panel, float animationTime = -1, float delayTime = 0f) {
		RectTransform rect = panel.GetComponent<RectTransform>();
		if (rect.offsetMin.x == rect.offsetMax.x) {
			instance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(-rect.rect.width, 0, 0), animationTime, delayTime);
		}
		else {
			instance.SlidePanel_Instance(panel, new Vector3(rect.rect.width / 2f, 0, 0), new Vector3(-rect.rect.width / 2f, 0, 0), animationTime, delayTime);
		}
	}

	public static void BringBackFromTop(GameObject panel, float animationTime = -1, float delayTime = 0f) {
		instance.SlidePanel_Instance(panel, new Vector3(0, panel.GetComponent<RectTransform>().rect.height, 0), Vector3.zero, animationTime, delayTime);
	}

	public static void BringBackFromBottom(GameObject panel, float animationTime = -1, float delayTime = 0f) {
		instance.SlidePanel_Instance(panel, new Vector3(0, -panel.GetComponent<RectTransform>().rect.height, 0), Vector3.zero, animationTime, delayTime);
	}

	public static void RemoveToTop(GameObject panel, float animationTime = -1, float delayTime = 0f) {
		instance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(0, panel.GetComponent<RectTransform>().rect.height, 0), animationTime, delayTime, true);
	}

	public static void RemoveToBottom(GameObject panel, float animationTime = -1, float delayTime = 0f) {
		instance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(0, -panel.GetComponent<RectTransform>().rect.height, 0), animationTime, delayTime, true);
	}

	void SlidePanel_Instance(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float animationTime = -1, float delayTime = 0f, bool removeElement = false) {
		StartCoroutine(SlidePanel_Routine(panel, initialPosition, finalPosition, animationTime, delayTime, removeElement));
	}

	#endregion
}