using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
		private Collider collider;
	private int counter = 0;
		//[SerializeField]
		//	[Range(i,j)]
		private void Awake ()
		{
				collider = gameObject.GetComponent<Collider> ();
		}

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
						//var temp = transform.rotation.eulerAngles.y + 1 * speed;
						var temp = transform.rotation.eulerAngles.y +  speed;
						transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, temp, transform.rotation.eulerAngles.z));
						print (counter);
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
