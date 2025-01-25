using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
	// שם הסצנה שאליה עוברים (תוכל להגדיר זאת דרך ה-Inspector או להחליף שם בקוד)
	[SerializeField] private string gameSceneName = "GameScene";

	// פונקציה שתופעל בעת לחיצה על הכפתור
	public void StartGame()
	{
		// טוען את הסצנה שהוגדרה
		SceneManager.LoadScene(gameSceneName);
	}
}
