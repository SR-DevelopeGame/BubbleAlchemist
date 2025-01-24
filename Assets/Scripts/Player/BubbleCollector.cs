using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollector : MonoBehaviour
{
    [Header("Bubble Settings")]
    [SerializeField] private int maxBubbles = 10; // ���� ������ ��������
    private int currentBubbleCount = 0; // ���� ������ ������

    [Header("Weapon Settings")]
    [SerializeField] private GameObject bubbleWeaponPrefab; // ������ ��� ������
    [SerializeField] private Transform weaponSpawnPoint; // ����� ����� ��� ������

    [Header("UI Feedback")]
    [SerializeField] private TMPro.TextMeshProUGUI bubbleCounterText; // ����� ����� ������ ������ (�� ����)

    void Start()
    {
        UpdateUI(); // ����� ���� ������ ������ �����
    }

    /// <summary>
    /// ������� ������� ���� ���� �����
    /// </summary>
    public void CollectBubble()
    {
        currentBubbleCount++; // ���� ���� �����
        UpdateUI(); // ���� ���� �����

        // ���� �� ���� ����� ��������
        if (currentBubbleCount >= maxBubbles)
        {
            CreateBubbleWeapon(); // ��� ��� �����
            ResetBubbleCount(); // ��� �� ���� ������
        }
    }

    /// <summary>
    /// ����� ��� ������
    /// </summary>
    private void CreateBubbleWeapon()
    {
        if (bubbleWeaponPrefab != null && weaponSpawnPoint != null)
        {
            Instantiate(bubbleWeaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation);
            Debug.Log("��� ����� ����!");
        }
        else
        {
            Debug.LogWarning("Bubble weapon prefab or spawn point not set!");
        }
    }

    /// <summary>
    /// ����� ���� ������
    /// </summary>
    private void ResetBubbleCount()
    {
        currentBubbleCount = 0;
        UpdateUI(); // ���� ���� �����
    }

    /// <summary>
    /// ����� ���� ������
    /// </summary>
    private void UpdateUI()
    {
        if (bubbleCounterText != null)
        {
            bubbleCounterText.text = $"Bubbles: {currentBubbleCount}/{maxBubbles}";
        }
    }

    /// <summary>
    /// ����� �� ����� ���� (���� ������)
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // ������ �� B ��� ����� ���� ������
        {
            CollectBubble();
        }
    }
}