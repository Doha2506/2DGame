using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
	float player2Speed = 0.2f;
	bool isJump;
	float jump = 20f;


	public Animator anim;
	public Rigidbody2D rigidbody2D;

	public Transform attackPoint;
	public float attackRange;
	public LayerMask playerLayers;

	public static float currentHealthBar;

	public Text winText2;


	[SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource AttackSound;
    [SerializeField] private AudioSource DeathSound;

    // Start is called before the first frame update
    void Start()
    {
        isJump = false;

        anim = GetComponent<Animator>();

        rigidbody2D = GetComponent<Rigidbody2D>();

		winText2.text = "";

		currentHealthBar = 0.05f;

	}

	private void PlayerMovement()
	{
		SpriteRenderer sR = GetComponent<SpriteRenderer>();
		bool player2MoveToRight = Input.GetKey(KeyCode.D);
		bool player2MoveToLeft = Input.GetKey(KeyCode.A);
		float horizontalAxis = Input.GetAxis("Horizontal2") * player2Speed;

		anim.SetFloat("PlayerSpeed", Mathf.Abs(horizontalAxis));
		if (player2MoveToRight)
		{
			GetComponent<Transform>().Translate(new Vector3(player2Speed, 0));
			sR.flipX = false;
		}
		if (player2MoveToLeft)
		{
			GetComponent<Transform>().Translate(new Vector3(-1 * player2Speed, 0));
			sR.flipX = true;
		}

	}

	private void PlayerJump()
	{
		bool jumping = Input.GetKey(KeyCode.Q);
		if (jumping && isJump == false)
		{
			jumpingSound.Play();
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jump);
			anim.SetBool("IsJump", true);
			isJump = true;
		}
		if (Mathf.Abs(rigidbody2D.velocity.y) < 0.01f)
		{
			anim.SetBool("IsJump", false);
			isJump = false;
		}
	}

	private void PlayerAttack()
	{
		bool attack = Input.GetKey(KeyCode.W);
		if (attack)
		{
			AttackSound.Play();

			anim.SetTrigger("Attack");

			Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

			try
			{
				foreach (Collider2D player in hitPlayers)
				{
					player.GetComponent<Player1>().TakeDamage();
				}

			}
			catch (NullReferenceException ex)
			{
				Debug.Log("Null in player 2");
			}


		}
	}
	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}


	public void TakeDamage()
	{
		//currentHealthBar = currentHealthBar - 0.001f;

		anim.SetTrigger("Hurt");

		if (currentHealthBar > 0)
		{
			currentHealthBar = currentHealthBar - 0.001f;
        }
        else
        {
			currentHealthBar = 0;

			Die();

			winText2.text = "Player 1 Win!!!";

			this.enabled = false;

		}

	}

	void Die()
	{
		anim.SetBool("IsDead", true);

		DeathSound.Play();
	}




	// Update is called once per frame
	void Update()
    {
		if (CompareTag("player2"))
		{
			PlayerMovement();

			PlayerJump();

			PlayerAttack();
		}
	}
}
