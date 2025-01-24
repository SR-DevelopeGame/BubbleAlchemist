using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	[SerializeField] private GameObject projectilePrefab; // הפריפאב של הקליע
	[SerializeField] private Transform firePoint; // נקודת הירי
	[SerializeField] private float minShootForce = 5f; // כוח ירי מינימלי
	[SerializeField] private float maxShootForce = 20f; // כוח ירי מקסימלי
	[SerializeField] private float chargeTime = 2f; // זמן הטעינה המקסימלי

	private float currentChargeTime = 4f; // זמן הטעינה הנוכחי
	private bool isCharging = false; // מצב האם השחקן טוען

	void Update()
	{
		// התחלת טעינה
		if (Input.GetKeyDown(KeyCode.Space)) // מחזיקים את מקש ה-Space
		{
			isCharging = true;
			currentChargeTime = 0f;
		}

		// טעינה
		if (isCharging)
		{
			currentChargeTime += Time.deltaTime;
			currentChargeTime = Mathf.Clamp(currentChargeTime, 0f, chargeTime);
		}

		// שחרור הירי
		if (Input.GetKeyUp(KeyCode.Space))
		{
			isCharging = false;
			Shoot();
		}
	}

	private void Shoot()
	{
		// חישוב כוח הירי בהתאם לזמן הטעינה
		float shootForce = Mathf.Lerp(minShootForce, maxShootForce, currentChargeTime / chargeTime);

		// יצירת הקליע
		GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

		// הפעלת הפיזיקה של הקליע
		Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
		if (rb != null)
		{
			rb.AddForce(firePoint.up * shootForce, ForceMode2D.Impulse);
		}

		// אפקטים נוספים (למשל, סאונד או חלקיקים) אפשר להוסיף כאן
	}
}
