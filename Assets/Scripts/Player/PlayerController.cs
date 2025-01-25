using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float speed = 5f; // מהירות התנועה קדימה
	[SerializeField]
	private float rotationSpeed = 100f; // מהירות הסיבוב

	private Rigidbody2D rb; // רכיב Rigidbody2D של השחקן

	void Start()
	{
		rb = GetComponent<Rigidbody2D>(); // קישור ה-Rigidbody2D
	}

	void Update()
	{
		HandleRotation(); // טיפול בסיבוב
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.W)) // תנועה קדימה רק כאשר W נלחץ
		{
			MoveForward();
		}
	}

	private void MoveForward()
	{
		// הזזת השחקן קדימה בכיוון שבו הוא פונה
		rb.MovePosition(rb.position + (Vector2)transform.up * speed * Time.fixedDeltaTime);
	}

	private void HandleRotation()
	{
		// סיבוב לצד שמאל עם KeyCode.A
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
		}
		// סיבוב לצד ימין עם KeyCode.D
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
		}
	}
}
