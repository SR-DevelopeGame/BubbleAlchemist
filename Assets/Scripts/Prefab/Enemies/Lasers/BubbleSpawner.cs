using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
	[SerializeField] private GameObject bubblePrefab; // פריפאב של הבועה
	[SerializeField] private Transform spawnPoint; // נקודת ההתחלה של הבועה
	[SerializeField] private float speed = 5f; // מהירות התנועה של הבועה
	[SerializeField] private float bubbleLifetime = 3f; // זמן החיים של הבועה
	[SerializeField] private float spawnInterval = 6f; // מרווח הזמן בין יצירת בועות
	[SerializeField] private float angle = 0f; // הזווית שבה הבועה תנוע (במעלות)
	

	private void Start()
	{
		// התחלת יצירת הבועות עם תחילת הסצנה
		SpawnBubble();

		// יצירת בועה חדשה כל X שניות
		InvokeRepeating(nameof(SpawnBubble), spawnInterval, spawnInterval);
	}

	private void SpawnBubble()
	{
		// יצירת הבועה במיקום ההתחלה
		GameObject bubble = Instantiate(bubblePrefab, spawnPoint.position, transform.rotation);

		// אפס את הסיבוב של הבועה
		//bubble.transform.rotation = Quaternion.identity;

		// הבטחה שתנועת הבועה היא בכיוון המבוקש
		Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
		if (rb != null)
		{
			// איפוס מהירות קיימת
			rb.velocity = Vector2.zero;

			// חישוב הכיוון לפי הזווית (מעלות -> רדיאנים)
			float radians = angle * Mathf.Deg2Rad; // המרה ממעלות לרדיאנים
			Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)); // כיוון התנועה

			// תנועה בכיוון המבוקש
			rb.velocity = direction.normalized * speed;
		}

		// השמדת הבועה לאחר זמן החיים שהוגדר
		Destroy(bubble, bubbleLifetime);
	}
}
