using UnityEngine;
using System.Collections;
using System;

public class TestDrug : MonoBehaviour
{
		[SerializeField]
		[Range(0.1f,10)]
		private float
				speed = 0.1f;
		[SerializeField]
		[Range(0.1f,10)]
		private float
				distance = 0.1f;
		private Vector3 posIn, posOut;
		Vector3 targetPoint;
		private bool flag;

		void OnMouseDown ()
		{
				print ("testDrag in ");
				posIn = Input.mousePosition;
		}

		void OnMouseUp ()
		{
				print ("testDrag out");
				posOut = Input.mousePosition;
				CalculateDirection ();
		}
		private void Update ()//вариант 2
		{
				if (!flag) {
						return;
				}
				if (Vector3.Distance (transform.position, targetPoint) > 0.1f) {
						transform.position = Vector3.MoveTowards (transform.position, targetPoint, speed * Time.deltaTime);					
				} else {
						flag = false;
				}
		}

		private void CalculateDirection ()
		{
				var xoffset = posOut.x - posIn.x;
				var yoffset = posOut.y - posIn.y;
				var absX = Mathf.Abs (xoffset);
				var absY = Mathf.Abs (yoffset);
				//Vector3 targetPoint;//вариант 1
				if (absX > absY) {
						//horizontal
						if (xoffset < 0) {
								print ("right");
								targetPoint = transform.position + new Vector3 (-distance, 0, 0);
						} else {
								print ("left");
								targetPoint = transform.position + new Vector3 (distance, 0, 0);
						}
				} else {
						//vertical
						if (yoffset < 0) {
								print ("down");
								targetPoint = transform.position + new Vector3 (0, -distance, 0);
						} else {
								print ("up");
								targetPoint = transform.position + new Vector3 (0, distance, 0);
						}
				}
				StartCoroutine (WaitAndDo (1f, () => {//вариант 3
						flag = true;
				}));
				//	flag = true;//вариант 2
				//	StartCoroutine (MoveToPoint (targetPoint)); вариант 1 задержкипо времени
		}

		private IEnumerator WaitAndDo (float time, Action action)//вариант 3
		{
				yield return new WaitForSeconds (time);
				if (action != null) {
						action ();
				}
		}

		/*	private IEnumerator MoveToPoint (Vector3 targetPoint)//интерфейс вариант 1 задержкипо времени
		{
				while (Vector3.Distance(transform.position,targetPoint)>0.1f) {
						transform.position = Vector3.MoveTowards (transform.position, targetPoint, speed * Time.deltaTime);
						yield return null;

				}
				print ("testdrag" + "at point");
		}*/
}
