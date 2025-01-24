using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private List<GameObject> weaponPrefabs; // ����� ����� ������
    private int currentWeaponIndex = 0; // ������ ���� ������

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
            currentWeaponIndex = 0; // ���� ������
        }
        else if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weaponPrefabs.Count - 1; // ���� ����
        }

        Debug.Log($"Current weapon: {weaponPrefabs[currentWeaponIndex].name}");
    }
}