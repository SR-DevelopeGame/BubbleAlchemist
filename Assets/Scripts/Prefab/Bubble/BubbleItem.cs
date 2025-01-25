
using UnityEngine;

public class BubbleItem : MonoBehaviour
{
	[SerializeField] private string bubbleType = "Soap"; // סוג הבועה

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			BubbleManager bubbleManager = FindObjectOfType<BubbleManager>();

			if (bubbleManager != null)
			{
				bubbleManager.CollectBubble(collision.gameObject, bubbleType);
				Destroy(gameObject); // השמדת הבועה
			}
			else
			{
				Debug.LogWarning("BubbleManager not found!");
			}
		}
	}
}
