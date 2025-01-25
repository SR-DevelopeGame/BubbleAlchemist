using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideBubble : MonoBehaviour
{
	public int damage = 10; // כמות הנזק שהירייה של האויב גורמת

	// הפונקציה מתבצעת כאשר יש התנגשות עם אובייקט אחר
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//// בדוק אם ההתנגשות היא עם השחקן
		if (collision.gameObject.CompareTag("Player"))
		{
			// קבל את הסקריפט של החיים מהשחקן
			PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

			if (playerHealth != null)
			{
				// הורד את החיים של השחקן רק אם לא נמחק
				playerHealth.TakeDamage(damage);
			}
		}

		if (gameObject != null)
		{
			Destroy(gameObject);
		}
	}
}
