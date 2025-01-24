using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBubble : MonoBehaviour
{
	[SerializeField] private float speed = 5f; // מהירות התנועה
	[SerializeField] private float lifetime = 3f; // זמן חיים של הבועה לפני היעלמות
	[SerializeField] private int damage = 5;

	private Rigidbody2D rb; // רכיב הפיזיקה
	private BubbleType bubbleType; // סוג הבועה (enum)


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	protected void Movement(BubbleType type)
	{
		switch (type)
		{
			case BubbleType.AddForce:
				MoveAddForce();
				break;

			case BubbleType.Straight:
				MoveStraight();
				break;

			default:
				Debug.LogWarning("Unknown BubbleType!");
				break;
		}
	}

	private void MoveAddForce()
	{
		rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
		StartCoroutine(DestroyAfterTime());
	}

	private void MoveStraight()
	{
		rb.velocity = transform.up * speed; // תנועה בקו ישר
		StartCoroutine(DestroyAfterTime());
	}

	private IEnumerator DestroyAfterTime()
	{
		// ממתינים לזמן החיים
		yield return new WaitForSeconds(lifetime);

		// הורסים את האובייקט
		Destroy(gameObject);
	}

	protected void Skill()
	{

	}

	// הפונקציה מתבצעת כאשר יש התנגשות עם אובייקט אחר
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// בדוק אם ההתנגשות היא עם השחקן
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// קבל את הסקריפט של החיים מהשחקן
			EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

			if (enemyHealth != null)
			{
				// הורד את החיים של השחקן רק אם לא נמחק
				enemyHealth.TakeDamage(damage);
			}
		}

		// השמד את הירייה לאחר ההתנגשות
		if (gameObject != null)
		{
			Destroy(gameObject);
		}
	}
}
