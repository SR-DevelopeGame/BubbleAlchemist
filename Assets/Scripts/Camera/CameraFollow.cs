using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform playerTransform; // רפרנס לשחקן
	[SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // מרחק בין המצלמה לשחקן
	[SerializeField] private float smoothSpeed = 0.125f; // מהירות התנועה החלקה

	void LateUpdate()
	{
		if (playerTransform == null) return; // אם אין שחקן, לא לעשות כלום

		// מיקום יעד של המצלמה
		Vector3 targetPosition = playerTransform.position + offset;

		// תנועה חלקה של המצלמה לעבר מיקום היעד
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

		// עדכון מיקום המצלמה
		transform.position = smoothedPosition;
	}
}
