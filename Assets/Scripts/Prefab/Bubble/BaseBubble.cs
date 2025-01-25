using UnityEngine;
using System.Collections;
using UnityEngine;



public class BaseBubble : MonoBehaviour
{
	[SerializeField] private float speed = 5f;
	[SerializeField] private float lifetime = 3f;
	[SerializeField] private int damage = 5;

	[SerializeField] private float initialForce = 10f;
	[SerializeField] private Vector2 launchDirection = new Vector2(1, 1);
	public BubbleType bubbleType; // סוג הבועה

	private Rigidbody2D rb;
	public bool canTeleportOnSecondShot = false;
	public int shootCooldown = 3;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Movement(bubbleType);
	}

	protected void Movement(BubbleType type)
	{
		switch (type)
		{
			case BubbleType.AddForce:
				MoveAddForce();
				Debug.Log("Print");
				break;

			case BubbleType.Straight:
				MoveStraight();
				break;

			case BubbleType.Teleport:
				MoveAddForce();
				Debug.Log("Print");
				break;

			default:
				Debug.LogWarning("Unknown BubbleType!");
				break;
		}
	}

	private void MoveAddForce()
	{
		rb.AddForce(transform.up * initialForce * Time.deltaTime, ForceMode2D.Impulse);
	}

	private void MoveStraight()
	{
		rb.velocity = transform.up * speed * Time.deltaTime;
		StartCoroutine(DestroyAfterTime());
	}

	private IEnumerator DestroyAfterTime()
	{
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(damage);
			}
		}

		Destroy(gameObject);
	}
}

