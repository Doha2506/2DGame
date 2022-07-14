using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    float speed = 0.2f;

    bool isJump;
    float jump = 20f;

    public Animator animator;
    public Rigidbody2D rigidbody2D;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    public static float currentHealthBar;

    public Text winText;

    public int currentHealth;


    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource AttackSound;
    [SerializeField] private AudioSource DeathSound;


    // Start is called before the first frame update
    void Start()
    {
        isJump = false;

        animator = GetComponent<Animator>();

        rigidbody2D = GetComponent<Rigidbody2D>();

        winText.text = "";

        currentHealthBar = 0.05f;
    }

    private void PlayerMovement()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        bool moveToRight = Input.GetKey(KeyCode.RightArrow);
        bool moveToLeft = Input.GetKey(KeyCode.LeftArrow);
        float horizontal = Input.GetAxis("Horizontal") * speed;


        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if (moveToRight)
        {
            GetComponent<Transform>().Translate(new Vector3(speed, 0));
            spriteRenderer.flipX = false;
        }
        if (moveToLeft)
        {
            GetComponent<Transform>().Translate(new Vector3(-1 * speed, 0));
            spriteRenderer.flipX = true;
        }
    }

    private void PlayerJump()
    {
        bool jumping = Input.GetKey(KeyCode.Space);
        if (jumping && isJump == false)
        {
            jumpingSound.Play();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jump);
            animator.SetBool("IsJump", true);
            isJump = true;
        }
        if (Mathf.Abs(rigidbody2D.velocity.y) < 0.01f)
        {
            animator.SetBool("IsJump", false);
            isJump = false;
        }
    }



	private void PlayerAttack()
	{
		bool up = Input.GetKey(KeyCode.UpArrow);
		if (up)
		{  

			AttackSound.Play();

			animator.SetTrigger("Attack");

			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


			try
			{
				foreach (Collider2D enemy in hitEnemies)
				{

					enemy.GetComponent<Player2>().TakeDamage();
                }

			}
			catch (NullReferenceException ex)
			{
				Debug.Log("Null in player 1");
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
        currentHealthBar = currentHealthBar - 0.001f;

        animator.SetTrigger("Hurt");

		if (currentHealthBar > 0)
		{
            currentHealthBar = currentHealthBar - 0.0001f;
        }
        else
        {

            currentHealthBar = 0;

            Die();

            winText.text = "Player 2 Win!!!";

            this.enabled = false;

        }

    }

	void Die()
	{
		animator.SetBool("IsDead", true);

		DeathSound.Play();

		this.enabled = false;
	}




	// Update is called once per frame
	void Update()
    {
        if (CompareTag("player1"))
        {
            PlayerMovement();

            PlayerJump();

            PlayerAttack();

            //Debug.Log("null");
        }
    }
}
