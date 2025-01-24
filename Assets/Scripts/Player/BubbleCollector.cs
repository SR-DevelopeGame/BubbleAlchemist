using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollector : MonoBehaviour
{
    [Header("Bubble Settings")]
    [SerializeField] private int maxBubbles = 10; // מספר הבועות המקסימלי
    private int currentBubbleCount = 0; // מספר הבועות שנאספו

    [Header("Weapon Settings")]
    [SerializeField] private GameObject bubbleWeaponPrefab; // פריפאב נשק הבועות
    [SerializeField] private Transform weaponSpawnPoint; // נקודת יצירת נשק הבועות

    [Header("UI Feedback")]
    [SerializeField] private TMPro.TextMeshProUGUI bubbleCounterText; // תצוגה למספר הבועות שנאספו (לא חובה)

    void Start()
    {
        UpdateUI(); // עדכון ממשק המשתמש בתחילת המשחק
    }

    /// <summary>
    /// פונקציה שמופעלת כאשר בועה נאספת
    /// </summary>
    public void CollectBubble()
    {
        currentBubbleCount++; // הוסף בועה למונה
        UpdateUI(); // עדכן ממשק משתמש

        // בדוק אם הגעת למספר המקסימלי
        if (currentBubbleCount >= maxBubbles)
        {
            CreateBubbleWeapon(); // צור נשק בועות
            ResetBubbleCount(); // אפס את מונה הבועות
        }
    }

    /// <summary>
    /// יצירת נשק הבועות
    /// </summary>
    private void CreateBubbleWeapon()
    {
        if (bubbleWeaponPrefab != null && weaponSpawnPoint != null)
        {
            Instantiate(bubbleWeaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation);
            Debug.Log("נשק בועות נוצר!");
        }
        else
        {
            Debug.LogWarning("Bubble weapon prefab or spawn point not set!");
        }
    }

    /// <summary>
    /// איפוס מונה הבועות
    /// </summary>
    private void ResetBubbleCount()
    {
        currentBubbleCount = 0;
        UpdateUI(); // עדכן ממשק משתמש
    }

    /// <summary>
    /// עדכון ממשק המשתמש
    /// </summary>
    private void UpdateUI()
    {
        if (bubbleCounterText != null)
        {
            bubbleCounterText.text = $"Bubbles: {currentBubbleCount}/{maxBubbles}";
        }
    }

    /// <summary>
    /// הדמיה של איסוף בועה (עבור בדיקות)
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // לוחצים על B כדי לאסוף בועה לבדיקה
        {
            CollectBubble();
        }
    }
}