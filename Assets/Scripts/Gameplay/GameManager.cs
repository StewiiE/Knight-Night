using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace S019745F
{
	public class GameManager : MonoBehaviour
	{
		public Arena1Manager arena1Manager;
		public int arenasCompleted = 0;
		public int enemiesRemaining = 0;

		public StarterGate starterGate;
		public SecondGate secondGate;

		public CanvasGroup infoPopup;
		public Text infoText;

		public bool canPassStarterGate = true;
		public bool canPassSecondGate = true;

		bool canShowControls = true;
		public GameObject controls;

		private void Update()
		{
			if (canPassStarterGate)
			{
				if (starterGate.gateTriggered == true)
				{
					infoPopup.alpha = 0f;
					canPassStarterGate = false;
				}
			}
			if (arena1Manager.canShowNextStats == true)
			{
				if(canPassSecondGate == true)
				{
					infoPopup.alpha = 1f;
					infoText.text = "Make your way to the next stage" + "\n" + "\n" + "\n" + "Break open some crates or barrels to find pickups!";
				}
			}
			if (canPassSecondGate)
			{
				if (secondGate.gateTriggered == true)
				{
					infoPopup.alpha = 0f;
					canPassSecondGate = false;
				}
			}


			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				if (canShowControls)
				{
					controls.SetActive(false);
					canShowControls = false;
				}
				else
				{
					controls.SetActive(true);
					canShowControls = true;
				}
			}
		}
	}
}
