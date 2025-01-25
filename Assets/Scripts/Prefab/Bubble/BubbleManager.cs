using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
	[SerializeField] private List<BubbleWeaponData> bubbleWeaponDataList; // רשימת סוגי הבועות והנשקים המתאימים
	private Dictionary<string, int> bubbleCounts = new Dictionary<string, int>(); // כמות בועות שנאספו לפי סוג

	private void Start()
	{
		// אתחול כמות הבועות שנאספו לכל סוג
		foreach (var data in bubbleWeaponDataList)
		{
			bubbleCounts[data.bubbleType] = 0;
		}
	}

	public void CollectBubble(GameObject player, string bubbleType)
	{
		if (!bubbleCounts.ContainsKey(bubbleType))
		{
			Debug.LogWarning($"Bubble type {bubbleType} is not defined!");
			return;
		}

		// עדכון כמות הבועות שנאספו
		bubbleCounts[bubbleType]++;
		Debug.Log($"Collected {bubbleType} bubble. Count: {bubbleCounts[bubbleType]}");

		// בדיקה אם הגיע הזמן ליצור נשק
		BubbleWeaponData bubbleWeaponData = bubbleWeaponDataList.Find(data => data.bubbleType == bubbleType);
		if (bubbleWeaponData != null && bubbleCounts[bubbleType] >= bubbleWeaponData.bubblesRequired)
		{
			AddBubbleWeapon(player, bubbleWeaponData.weaponPrefab);
			bubbleCounts[bubbleType] = 0; // איפוס הכמות
		}
	}

	private void AddBubbleWeapon(GameObject player, GameObject weaponPrefab)
	{
		WeaponManager weaponManager = player.GetComponent<WeaponManager>();

		if (weaponManager != null && weaponPrefab != null)
		{
			weaponManager.AddWeapon(weaponPrefab);
			Debug.Log($"Weapon of type {weaponPrefab.name} added to player!");
		}
		else
		{
			Debug.LogWarning("WeaponManager is missing or weapon prefab is null!");
		}
	}
}