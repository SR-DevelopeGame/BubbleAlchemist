using UnityEngine;
using UnityEngine.SceneManagement; // דרוש עבור ניהול סצנות

public class LevelChanger : MonoBehaviour
{
	// אם השחקן נוגע באובייקט, נעבור לשלב הבא
	private void OnTriggerEnter2D(Collider2D other)
	{
		// בדוק אם זה השחקן
		if (other.CompareTag("Player"))
		{
			// טוען את השלב הבא (כלומר, השלב הבא בסצנה)
			LoadNextLevel();
		}
	}

	private void LoadNextLevel()
	{
		// טוען את השלב הבא בסצנה לפי אינדקס
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

		// אם יש שלב הבא
		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(nextSceneIndex); // טוען את השלב הבא
		}
		else
		{
			Debug.Log("No more levels to load.");
		}
	}
}
