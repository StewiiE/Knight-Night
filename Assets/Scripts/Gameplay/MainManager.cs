using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
	public GameObject canvas;

	private static bool UIExists;

	private void Start()
	{
		if (!UIExists)
		{
			UIExists = true;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void Update ()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			//canvas.SetActive(false);
			Time.timeScale = 1f;
		}
		else
		{
			//canvas.SetActive(true);
		}
	}
}
