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

	void Update()
	{
		if (showOutline == true)
		{
			if (outlineMainGO != null)
			{
				outlineMainGO.GetComponent<Outline>().enabled = true;
			}
			if (outlineHeadGO != null)
			{
				outlineHeadGO.GetComponent<Outline>().enabled = true;
			}
			if (outlineShieldGO != null)
			{
				outlineShieldGO.GetComponent<Outline>().enabled = true;
			}
			if (outlineSwordGO != null)
			{
				outlineSwordGO.GetComponent<Outline>().enabled = true;
			}
		}
		else
		{
			if (outlineMainGO != null)
			{
				outlineMainGO.GetComponent<Outline>().enabled = false;
			}
			if (outlineHeadGO != null)
			{
				outlineHeadGO.GetComponent<Outline>().enabled = false;
			}
			if (outlineShieldGO != null)
			{
				outlineShieldGO.GetComponent<Outline>().enabled = false;
			}
			if (outlineSwordGO != null)
			{
				outlineSwordGO.GetComponent<Outline>().enabled = false;
			}
		}
	}
}


