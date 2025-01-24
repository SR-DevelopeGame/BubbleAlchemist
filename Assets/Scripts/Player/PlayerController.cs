using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float speed = 5f; // מהירות התנועה
	[SerializeField]
	private float rotationSpeed = 100f; // מהירות הסיבוב

	private Vector2 movement; // כיוון התנועה
	private Rigidbody2D rb; // רכיב Rigidbody2D של השחקן

	void Start()
	{
		rb = GetComponent<Rigidbody2D>(); // קישור ה-Rigidbody2D
	}

	void Update()
	{
		Movement();
		HandleRotation();
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
	}

	private void Movement()
	{
		// בדיקת קלט לתנועה
		if (Input.GetKey(KeyCode.A)) // תנועה שמאלה
		{
			movement = Vector2.left; // קביעת כיוון התנועה שמאלה
		}
		else if (Input.GetKey(KeyCode.D)) // תנועה ימינה
		{
			movement = Vector2.right; // כיוון התנועה ימינה
		}
		else if (Input.GetKey(KeyCode.W)) // תנועה למעלה
		{
			movement = Vector2.up; // כיוון התנועה למעלה
		}
		else if (Input.GetKey(KeyCode.S)) // תנועה למטה
		{
			movement = Vector2.down; // כיוון התנועה למטה
		}
		else
		{
			movement = Vector2.zero; // אין תנועה
		}
	}

	private void HandleRotation()
	{
		// סיבוב לצד ימין עם KeyCode.E
		if (Input.GetKey(KeyCode.E))
		{
			transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
		}
		// סיבוב לצד שמאל עם KeyCode.Q
		else if (Input.GetKey(KeyCode.Q))
		{
			transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
		}
	}
}
