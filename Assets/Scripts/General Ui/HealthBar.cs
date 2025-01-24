using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;

	// פונקציה שמגדירה את ערך המקסימום של החיים
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

	// פונקציה שמעדכנת את ערך החיים הנוכחי
	public void SetHealth(int health)
	{
		slider.value = health;
	}
}
