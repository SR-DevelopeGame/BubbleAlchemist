using UnityEngine;

public class PlayerPersistence : MonoBehaviour
{
	private static PlayerPersistence instance; // שמירת אינסטנס יחיד של השחקן

	private void Awake()
	{
		// בדיקה אם כבר קיים אינסטנס של האובייקט הזה
		if (instance == null)
		{
			instance = this; // הגדרת האינסטנס הנוכחי
			DontDestroyOnLoad(gameObject); // הבטחה שהאובייקט לא ייהרס כשעוברים סצנה
		}
		else
		{
			Destroy(gameObject); // אם כבר קיים אינסטנס, הורסים את האובייקט החדש
		}
	}
	public void SetPosition(Vector3 newPosition)
	{
		transform.position = newPosition; // מעדכן את מיקום השחקן
	}
}
