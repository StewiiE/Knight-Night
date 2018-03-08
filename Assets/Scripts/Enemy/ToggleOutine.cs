using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class ToggleOutine : MonoBehaviour
{
	public GameObject outlineMainGO;
	public GameObject outlineHeadGO;
	public GameObject outlineShieldGO;
	public GameObject outlineSwordGO;
	public bool showOutline = false;
	
	void Update ()
	{
		if(showOutline == true)
		{
			outlineMainGO.GetComponent<Outline>().enabled = true;
			outlineHeadGO.GetComponent<Outline>().enabled = true;
			outlineShieldGO.GetComponent<Outline>().enabled = true;
			outlineSwordGO.GetComponent<Outline>().enabled = true;
		}
		else
		{
			outlineMainGO.GetComponent<Outline>().enabled = false;
			outlineHeadGO.GetComponent<Outline>().enabled = false;
			outlineShieldGO.GetComponent<Outline>().enabled = false;
			outlineSwordGO.GetComponent<Outline>().enabled = false;
		}
	}
}


