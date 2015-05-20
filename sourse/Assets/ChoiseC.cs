using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ChoiseC : MonoBehaviour {

    public static int choise;
	private Collider collider;
	//public event Action<Card> OnButtonClick;

	private void Awake ()
	{
		collider = gameObject.GetComponent<Collider> ();
	}
 /*   private void OnMouseDown()
    {
        print("OnMouseDown "+gameObject.name);
        choise = Convert.ToInt16(gameObject.name); 
    }*/
	#region TRANSFORM
	#endregion

/*	private	void OnMouseDown ()
	{
		//	print ("OnMouseDown");
		var temp = GetComponent<Card> ();//ссылка на скрипт Card
		OnButtonClickHandler (temp);
	}
	private void OnButtonClickHandler (Card card)
	{
		if (OnButtonClick == null)
			return;
		
		OnButtonClick (card);
	}*/

	public void Flip ()
	{
		print ("Card" + "Flip");
		StopAllCoroutines ();
		StartCoroutine (StartFlip ());
	}
	private IEnumerator StartFlip ()
	{
		if (collider != null) {
			collider.enabled = false;
		}
		
		int counter = 0;
		int speed = 20;
		while (true) {
			var temp = transform.rotation.eulerAngles.y + 1 * speed;
			transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, temp, transform.rotation.eulerAngles.z));
			counter ++;
			if (counter == 180 / speed) {
				break;
			}
			yield return null;
		}
		
		if (collider != null) {
			collider.enabled = true;
		}
		
	}

}
