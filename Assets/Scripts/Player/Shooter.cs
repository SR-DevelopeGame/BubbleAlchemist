
using UnityEngine;

using UnityEngine;

public class Shooter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private WeaponManager weaponManager; // רפרנס למנהל הנשקים
	[SerializeField] private Transform firePoint; // נקודת הירי
	[SerializeField] private float chargeTime = 2f; // זמן טעינה מקסימלי

	private float currentChargeTime = 0f; // זמן הטעינה הנוכחי
	private bool isCharging = false; // האם השחקן טוען
	private bool isTeleporting = false; // משתנה למעקב אחרי מצב ההשתגרות
	private bool canShoot = true; // האם השחקן יכול לירות
	private int shotsFired = 0; // משתנה לספירת היריות

	private Vector3 lastFirePosition; // המיקום של הירי האחרון
	private float lastShotTime = 0f; // זמן הירי האחרון

	void Update()
	{
		HandleInput();
	}

	private void HandleInput()
	{
		// טעינת נשק
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCharging();
		}

		// החלפת נשק
		if (Input.GetKeyDown(KeyCode.RightArrow)) weaponManager.SwitchWeapon(1);
		if (Input.GetKeyDown(KeyCode.LeftArrow)) weaponManager.SwitchWeapon(-1);

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

	private void StartCharging()
	{
		isCharging = true;
		currentChargeTime = 0f;
	}

	private void Shoot()
	{
		// אם השחקן לא יכול לירות (הוא במצב השתגרות או לא עבר זמן מספיק)
		if (!canShoot || isTeleporting || !CanShootAgain())
		{
			return;
		}

		GameObject currentWeaponPrefab = weaponManager.GetCurrentWeapon();
		if (currentWeaponPrefab == null)
		{
			Debug.LogWarning("No weapon selected!");
			return;
		}

		BaseBubble bubble = currentWeaponPrefab.GetComponent<BaseBubble>();
		if (bubble != null && bubble.canTeleportOnSecondShot)
		{
			HandleTeleportingWeapon(bubble);
		}
		else
		{
			FireRegularWeapon(currentWeaponPrefab);
		}

		// עדכון זמן הירי האחרון
		lastShotTime = Time.time;
	}

	private bool CanShootAgain()
	{
		GameObject currentWeaponPrefab = weaponManager.GetCurrentWeapon();
		if (currentWeaponPrefab == null)
		{
			return false;
		}

		// בודקים את זמן ה-Cooldown של הנשק
		BaseBubble bubble = currentWeaponPrefab.GetComponent<BaseBubble>();
		if (bubble != null)
		{
			return (Time.time - lastShotTime) >= bubble.shootCooldown;
		}
		return true;
	}

	private void HandleTeleportingWeapon(BaseBubble bubble)
	{
		if (shotsFired == 0)
		{
			// ירי ראשון
			InstantiateWeapon();
			shotsFired++;
			lastFirePosition = firePoint.position; // שומרים את המיקום של הירי הראשון
		}
		else if (shotsFired == 1)
		{
			// ירי שני - השתגרות
			StartTeleporting();
		}
	}

	private void FireRegularWeapon(GameObject currentWeaponPrefab)
	{
		InstantiateWeapon();
	}

	private void InstantiateWeapon()
	{
		GameObject currentWeaponPrefab = weaponManager.GetCurrentWeapon();
		Instantiate(currentWeaponPrefab, firePoint.position, firePoint.rotation);
	}

	private void StartTeleporting()
	{
		Debug.Log("Teleporting...");
		isTeleporting = true;
		canShoot = false; // לא ניתן לירות בזמן ההשתגרות
		TeleportToLastFirePosition();
		ResetShotState();
	}

	private void TeleportToLastFirePosition()
	{
		transform.position = lastFirePosition;
		Debug.Log("Teleported to last fire position!"); // הודעה לאחר השתגרות

		// אתחול מצב השתגרות ויאפשר ירי מחדש
		isTeleporting = false;
		canShoot = true; // אפשר לירות מחדש אחרי ההשתגרות
	}

	private void ResetShotState()
	{
		shotsFired = 0; // אתחול ספירת היריות אחרי השתגרות
	}
}