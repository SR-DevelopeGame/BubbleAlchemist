using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[Header("Movement Settings")]
	public float moveSpeed = 2f; // מהירות התנועה
	public float moveRange = 5f; // הטווח שבו האויב זז
	private Vector3 initialPosition; // מיקום ההתחלה של האויב
	private bool movingRight = true; // האם האויב זז ימינה
	private bool isChangingDirection = false; // האם האויב משנה כיוון כרגע

	[Header("Shooting Settings")]
	public GameObject projectilePrefab; // פריפאב של הירייה
	public Transform firePoint; // נקודת הירי
	public float shootInterval = 2f; // זמן בין יריות
	public float projectileSpeed = 5f; // מהירות הירייה

	[Header("Target Settings")]
	public float detectionRange = 10f; // טווח גילוי השחקן
	private Transform player; // השחקן (המטרה)

	// מבצע את ה-Flip על ה- SpriteRenderer
	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		// שמירת מיקום ההתחלה
		initialPosition = transform.position;

		// חיפוש אוטומטי של השחקן לפי תג
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
		if (playerObject != null)
		{
			player = playerObject.transform;
		}
		else
		{
			Debug.LogWarning("Player not found! Make sure the Player object has the tag 'Player'.");
		}

		// התחלת הירי
		StartCoroutine(ShootAtPlayer());
		// התחלת שינוי כיוונים כל כמה שניות
		StartCoroutine(ChangeDirection());

		// קישור ה- SpriteRenderer
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		// אם האויב זז ימינה
		if (movingRight)
		{
			transform.position += Vector3.right * moveSpeed * Time.deltaTime;
		}
		// אם האויב זז שמאלה
		else
		{
			transform.position += Vector3.left * moveSpeed * Time.deltaTime;
		}
	}

	// ירי לעבר השחקן
	private IEnumerator ShootAtPlayer()
	{
		while (true)
		{
			// בדוק אם השחקן בטווח
			if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
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
		Debug.Log("Projectile Fired");

		// חישוב כיוון הירי
		Vector3 direction = (player.position - firePoint.position).normalized;

		// הוספת מהירות לפרויקטיל
		Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
		if (rb != null)
		{
			rb.velocity = direction * projectileSpeed;
		}
	}

	// פונקציה לשינוי כיוון כל כמה שניות
	private IEnumerator ChangeDirection()
	{
		while (true)
		{
			// ממתין במשך הזמן שהוגדר
			yield return new WaitForSeconds(Random.Range(2f, 5f)); // זמן אקראי לשינוי כיוון

			// שינוי כיוון
			movingRight = !movingRight;

			// Flip של ה-Sprite בהתאם לכיוון
			FlipSprite();
		}
	}

	// פונקציה שתהפוך את ה- Sprite לפי הכיוון
	private void FlipSprite()
	{
		// אם האויב זז ימינה, משנה את הסקלה X ל-1, אם לא אז -1
		if (movingRight)
		{
			spriteRenderer.flipX = false; // תראה את הספראייט כרגיל
		}
		else
		{
			spriteRenderer.flipX = true; // הפוך את הספראייט
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// בדוק אם ההתנגשות היא עם קיר
		if (collision.gameObject.CompareTag("Wall"))
		{
			movingRight = !movingRight;
			FlipSprite();
		}
	}
}
