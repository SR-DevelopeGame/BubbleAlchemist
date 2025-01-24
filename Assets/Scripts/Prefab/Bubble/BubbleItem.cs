using UnityEngine;

public class BubbleItem : MonoBehaviour
{
    [SerializeField] private string bubbleType = "Soap"; // ��� ����� (������: ����)

    public delegate void BubbleCollected(GameObject player, string bubbleType);
    public static event BubbleCollected OnBubbleCollected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����� �� ����� ���� ��������
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bubble collected!");

            // ����� ������ ������
            OnBubbleCollected?.Invoke(collision.gameObject, bubbleType);

            // ����� �����
            Destroy(gameObject);
        }
    }
}