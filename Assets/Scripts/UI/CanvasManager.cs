using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

	private void Update()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			this.gameObject.SetActive(false);
		}
		else
		{
			this.gameObject.SetActive(true);
		}
	}
}
