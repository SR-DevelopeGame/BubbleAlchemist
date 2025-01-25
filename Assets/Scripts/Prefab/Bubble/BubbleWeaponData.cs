using UnityEngine;

[System.Serializable]
public class BubbleWeaponData
{
	public string bubbleType; // סוג הבועה (לדוגמה "Soap", "Fire")
	public GameObject weaponPrefab; // הנשק שיווצר
	public int bubblesRequired; // כמה בועות נדרשות לנשק
	public bool canTeleportOnSecondShot; // האם הנשק כולל את יכולת ההשתגרות
}