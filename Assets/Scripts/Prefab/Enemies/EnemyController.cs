using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[Header("Movement Settings")]
	public float moveSpeed = 2f; // מהירות התנועה
	public float moveRange = 5f; // הטווח שבו האויב זז
	private Vector3 initialPosition; // מיקום ההתחלה של האויב
	private bool movingRight = true; // האם האויב זז ימינה

	[Header("Shooting Settings")]
	public GameObject projectilePrefab; // פריפאב של הירייה
	public Transform firePoint; // נקודת הירי
	public float shootInterval = 2f; // זמן בין יריות
	public float projectileSpeed = 5f; // מהירות הירייה

	[Header("Target Settings")]
	public Transform player; // השחקן (המטרה)
	public float detectionRange = 10f; // טווח גילוי השחקן

	private void Start()
	{
		// שמירת מיקום ההתחלה
		initialPosition = transform.position;

		// התחלת הירי
		StartCoroutine(ShootAtPlayer());
	}

	private void Update()
	{
		if (player != null)
		{
			MoveEnemy();
		}
		
	}

	// תנועה אוטומטית של האויב
	private void MoveEnemy()
	{
		if (movingRight)
		{
			transform.position += Vector3.right * moveSpeed * Time.deltaTime;

			// בדיקה אם האויב הגיע לטווח הימני
			if (transform.position.x >= initialPosition.x + moveRange)
			{
				movingRight = false;
			}
		}
		else
		{
			transform.position += Vector3.left * moveSpeed * Time.deltaTime;

			// בדיקה אם האויב הגיע לטווח השמאלי
			if (transform.position.x <= initialPosition.x - moveRange)
			{
				movingRight = true;
			}
		}
	}

	// ירי לעבר השחקן
	private IEnumerator ShootAtPlayer()
	{
		
		while (true && player != null)
		{
			// בודק אם השחקן בטווח
			if (Vector3.Distance(transform.position, player.position) <= detectionRange)
			{
				Shoot();
			}

			// ממתין לזמן בין יריות
			yield return new WaitForSeconds(shootInterval);
		}
		
 
	
	}

	// פונקציה שיוצרת פרויקטיל
	private void Shoot()
	{
		// יצירת פרויקטיל בנקודת הירי
		GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

		// חישוב כיוון הירי
		Vector3 direction = (player.position - firePoint.position).normalized;

		// הוספת מהירות לפרויקטיל
		Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
		if (rb != null)
		{
			rb.velocity = direction * projectileSpeed;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// בדוק אם ההתנגשות היא עם השחקן
		if (collision.gameObject.CompareTag("Wall"))
		{
			if(movingRight)
			{
				movingRight = false;
			}
			else
			{
				movingRight = true;
			}
		}
	}
}
