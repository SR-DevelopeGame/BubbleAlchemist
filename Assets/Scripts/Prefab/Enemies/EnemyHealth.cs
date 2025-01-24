using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[Header("Health Settings")]
	public int maxHealth = 100; // כמות החיים המקסימלית של השחקן
	private int currentHealth;  // החיים הנוכחיים של השחקן

	[Header("UI Settings")]
	public HealthBar healthBar; // הפניה לסרגל החיים (אופציונלי)

	private void Start()
	{
		// הגדרת החיים הנוכחיים ל-100% בתחילת המשחק
		currentHealth = maxHealth;

		// עדכון סרגל החיים (אם יש)
		if (healthBar != null)
		{
			healthBar.SetMaxHealth(maxHealth);
		}
	}

	// פונקציה שמורידה חיים לשחקן
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		// ודא שהחיים לא יורדים מתחת ל-0
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		// עדכון סרגל החיים
		if (healthBar != null)
		{
			healthBar.SetHealth(currentHealth);
		}

		// בדיקה אם השחקן מת
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	// פונקציה שמחזירה חיים לשחקן
	public void Heal(int amount)
	{
		currentHealth += amount;

		// ודא שהחיים לא עולים מעבר למקסימום
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		// עדכון סרגל החיים
		if (healthBar != null)
		{
			healthBar.SetHealth(currentHealth);
		}
	}

	// פונקציה לטיפול במצב של הפסד חיים מלא
	private void Die()
	{
		Debug.Log("Enemy has died!");

		// כאן ניתן להוסיף אנימציה של מוות, מסך הפסד, או לטעון סצנה מחדש
		Destroy(gameObject); // לדוגמה: הסרת האובייקט
	}
}
