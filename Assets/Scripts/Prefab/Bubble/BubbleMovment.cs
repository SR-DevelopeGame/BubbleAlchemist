using System.Collections;
using UnityEngine;

public class BubbleMovment : MonoBehaviour
{
	private Rigidbody2D rb; // רכיב הפיזיקה
	public float speed = 5f; // מהירות התנועה
	public float lifetime = 3f; // זמן חיים של הבועה לפני היעלמות

	void Start()
	{
	
		rb = GetComponent<Rigidbody2D>();

		rb.AddForce(transform.right * speed, ForceMode2D.Impulse);

		StartCoroutine(DestroyAfterTime());
	}

	void Update()
	{
	
	}

	private IEnumerator DestroyAfterTime()
	{
		// ממתינים לזמן החיים
		yield return new WaitForSeconds(lifetime);

		// הורסים את האובייקט
		Destroy(gameObject);
	}
}
