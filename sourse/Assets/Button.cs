using UnityEngine;
using System;
using System.Collections;

public class Button : MonoBehaviour
{
		public event Action<Card> OnButtonClick;

		private void OnButtonClickHandler (Card card)
		{
				if (OnButtonClick == null)
						return;

				OnButtonClick (card);
		}

		private	void OnMouseDown ()
		{
				//	print ("OnMouseDown");
				var temp = GetComponent<Card> ();//ссылка на скрипт Card
				OnButtonClickHandler (temp);
		}
}
