using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
	[SerializeField] private GameObject startPosition;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//// בדוק אם ההתנגשות היא עם השחקן
		if (collision.gameObject.CompareTag("BubbleEnemy"))
		{
			// קבל את הסקריפט של החיים מהשחקן
			//PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

			Destroy(collision.gameObject);
			transform.position = startPosition.transform.position;
		}
		else if(collision.gameObject.CompareTag("Enemy"))
		{
			transform.position = startPosition.transform.position;
		}
	}
}
