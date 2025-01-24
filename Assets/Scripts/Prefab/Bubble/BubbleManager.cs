using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private GameObject bubbleWeaponPrefab; // פריפאב של נשק הבועות
    [SerializeField] private int maxBubbles = 10; // מספר הבועות המקסימלי

    private int currentBubbleCount = 0; // מספר הבועות שנאספו

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
            currentBubbleCount = 0; // איפוס מונה הבועות
        }
    }

    private void AddBubbleWeapon(GameObject player)
    {
        // חיפוש WeaponManager על השחקן
        WeaponManager weaponManager = player.GetComponent<WeaponManager>();

        if (weaponManager != null && bubbleWeaponPrefab != null)
        {
            weaponManager.AddWeapon(bubbleWeaponPrefab); // הוספת נשק חדש
            Debug.Log("Bubble weapon added!");
        }
        else
        {
            Debug.LogWarning("Bubble weapon prefab is missing or player does not have a WeaponManager component!");
        }
    }
}