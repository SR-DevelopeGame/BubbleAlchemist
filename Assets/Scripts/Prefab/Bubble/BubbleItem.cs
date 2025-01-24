using UnityEngine;

public class BubbleItem : MonoBehaviour
{
    [SerializeField] private string bubbleType = "Soap"; // סוג הבועה (לדוגמה: סבון)

    public delegate void BubbleCollected(GameObject player, string bubbleType);
    public static event BubbleCollected OnBubbleCollected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // בדיקה אם השחקן נכנס לאובייקט
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bubble collected!");

            // קריאה לאירוע האיסוף
            OnBubbleCollected?.Invoke(collision.gameObject, bubbleType);

            // השמדת הבועה
            Destroy(gameObject);
        }
    }
}