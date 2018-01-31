﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonUIComponent : MonoBehaviour
{
	public Text locationText;
	public Text timeText; //timeText.text = System.DateTime.Now.ToString("h:mm:ss tt");  //May cause garbage collection problem
	public Text yearText;
	public Text eraText; // TODO remove?
	public GameObject helpHintGO;
	public GameObject helpFullGO;

	public float helpHintStartDelay = 4.0f;
	public float helpHintDuration = 5.0f;

	private void Start()
	{
		helpHintGO.SetActive(false);
		helpFullGO.SetActive(false);
		Invoke("OnStartHint", helpHintStartDelay);
	}

	private void OnEnable()
	{
		Missive.AddListener<YearDataMissive>(OnYearData);
		Missive.AddListener<HelpMissive>(OnHelp);
	}

	private void OnDisable()
	{
		Missive.RemoveListener<YearDataMissive>(OnYearData);
		Missive.RemoveListener<HelpMissive>(OnHelp);
	}

	private void OnYearData(YearDataMissive missive)
	{
		yearText.text = missive.data.year.ToString();
	}

	private void OnStartHint()
	{
		helpHintGO.SetActive(true);
	}

	private void CloseHelpFullGO()
	{
		helpFullGO.SetActive(false);
	}

	private void OnHelp(HelpMissive missive)
	{
		if (helpFullGO.activeInHierarchy)
		{
			helpFullGO.SetActive(false);
			CancelInvoke("CloseHelpFullGO");
		}
		else
		{
			helpFullGO.SetActive(true);
			Invoke("CloseHelpFullGO", helpHintDuration);
		}
	}

	private void OnDestroy()
	{
		CancelInvoke();
	}
}
