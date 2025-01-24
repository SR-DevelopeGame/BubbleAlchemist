using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponManager weaponManager; // רפרנס למנהל הנשקים
    [SerializeField] private Transform firePoint; // נקודת הירי
    [SerializeField] private float minShootForce = 5f; // כוח ירי מינימלי
    [SerializeField] private float maxShootForce = 20f; // כוח ירי מקסימלי
    [SerializeField] private float chargeTime = 2f; // זמן טעינה מקסימלי

    private float currentChargeTime = 0f; // זמן הטעינה הנוכחי
    private bool isCharging = false; // האם השחקן טוען

    void Update()
    {
        // טעינת נשק
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentChargeTime = 0f;
        }

        // החלפת נשק
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            weaponManager.SwitchWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            weaponManager.SwitchWeapon(-1);
        }

        // טעינה
        if (isCharging)
        {
            currentChargeTime += Time.deltaTime;
            currentChargeTime = Mathf.Clamp(currentChargeTime, 0f, chargeTime);
        }

        // שחרור הירי
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isCharging = false;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject currentWeaponPrefab = weaponManager.GetCurrentWeapon();
        if (currentWeaponPrefab != null)
        {
            // יצירת קליע
            GameObject projectile = Instantiate(currentWeaponPrefab, firePoint.position, firePoint.rotation);
            Debug.Log($"Fired weapon: {currentWeaponPrefab.name}");
        }
        else
        {
            Debug.LogWarning("No weapon selected!");
        }
    }
}