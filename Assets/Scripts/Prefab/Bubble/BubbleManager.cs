using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private GameObject bubbleWeaponPrefab; // ������ �� ��� ������
    [SerializeField] private int maxBubbles = 10; // ���� ������ ��������

    private int currentBubbleCount = 0; // ���� ������ ������

    private void OnEnable()
    {
        BubbleItem.OnBubbleCollected += HandleBubbleCollected;
    }

    private void OnDisable()
    {
        BubbleItem.OnBubbleCollected -= HandleBubbleCollected;
    }

    private void HandleBubbleCollected(GameObject player, string bubbleType)
    {
        currentBubbleCount++;

        Debug.Log($"Bubble collected: {currentBubbleCount}/{maxBubbles}");

        if (currentBubbleCount >= maxBubbles)
        {
            AddBubbleWeapon(player);
            currentBubbleCount = 0; // ����� ���� ������
        }
    }

    private void AddBubbleWeapon(GameObject player)
    {
        // ����� WeaponManager �� �����
        WeaponManager weaponManager = player.GetComponent<WeaponManager>();

        if (weaponManager != null && bubbleWeaponPrefab != null)
        {
            weaponManager.AddWeapon(bubbleWeaponPrefab); // ����� ��� ���
            Debug.Log("Bubble weapon added!");
        }
        else
        {
            Debug.LogWarning("Bubble weapon prefab is missing or player does not have a WeaponManager component!");
        }
    }
}