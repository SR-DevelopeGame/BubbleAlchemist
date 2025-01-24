using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private List<GameObject> weaponPrefabs; // רשימת נשקים זמינים
    private int currentWeaponIndex = 0; // אינדקס הנשק הנוכחי

    public GameObject GetCurrentWeapon()
    {
        return weaponPrefabs[currentWeaponIndex];
    }

    public void AddWeapon(GameObject newWeaponPrefab)
    {
        if (!weaponPrefabs.Contains(newWeaponPrefab))
        {
            weaponPrefabs.Add(newWeaponPrefab);
            Debug.Log($"Weapon {newWeaponPrefab.name} added!");
        }
    }

    public void SwitchWeapon(int direction)
    {
        currentWeaponIndex += direction;

        if (currentWeaponIndex >= weaponPrefabs.Count)
        {
            currentWeaponIndex = 0; // חזרה להתחלה
        }
        else if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weaponPrefabs.Count - 1; // חזרה לסוף
        }

        Debug.Log($"Current weapon: {weaponPrefabs[currentWeaponIndex].name}");
    }
}