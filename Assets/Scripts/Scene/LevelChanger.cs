using UnityEngine;
using UnityEngine.SceneManagement; // דרוש עבור ניהול סצנות

public class LevelChanger : MonoBehaviour
{

	[SerializeField] private Vector3 playerStartPosition;
	[SerializeField] private string nextSceneName;
	private void OnTriggerEnter2D(Collider2D other)
	{
		// בדוק אם זה השחקן
		if (other.CompareTag("Player"))
		{
			// טוען את השלב הבא (כלומר, השלב הבא בסצנה)
			LoadNextLevel();
		}
	}

	public void LoadNextLevel()
	{
		// טוען את השלב הבא בסצנה לפי אינדקס
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

		// אם יש שלב הבא
		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			PlayerPersistence player = FindObjectOfType<PlayerPersistence>();
			SceneManager.LoadScene(nextSceneIndex); // טוען את השלב הבא
			ClearAllBubbles();
			if (player != null)
			{
				player.SetPosition(playerStartPosition);
			}
		}
		else
		{
			Debug.Log("No more levels to load.");
		}
	}

	public void ClearAllBubbles()
	{
		// מציאת כל האובייקטים בסצנה עם הסקריפט BaseBubble
		BaseBubble[] bubbles = FindObjectsOfType<BaseBubble>();

		// לולאה על כל הבועות והשמדתן
		foreach (BaseBubble bubble in bubbles)
		{
			Destroy(bubble.gameObject);
		}
	}

}
