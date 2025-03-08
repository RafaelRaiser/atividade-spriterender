using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private Animator animator;

    public float health = 100f;
    private Vector2 movement;
    private int killCount = 0;

    private void Start()
    {
        // Configurar o Rigidbody2D
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Adicionar e configurar o Collider2D
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }

        UIManager.Instance.UpdateHealth(health);
        UIManager.Instance.UpdateKillCount(killCount);
    }

    private void Update()
    {
        // Captura a entrada do jogador
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        // Rotacionar o arco para seguir o cursor do mouse
        RotateTowardsMouse();

        // Dispara a bala
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        // Move o jogador com base na entrada capturada
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        // Iniciar a animação de tiro
        if (animator != null)
        {
            animator.SetBool("Shoot", true);
        }

        // Instanciar a bala
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Definir a direção da bala como a direção do bulletPoint
        Vector2 direction = bulletPoint.right;
        bulletScript.SetDirection(direction);

        // Passar a referência do Player para a bala
        bulletScript.SetPlayer(this);
    }

    private void RotateTowardsMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)bulletPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        UIManager.Instance.UpdateHealth(health);
        if (health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        UIManager.Instance.ShowGameOver();
    }

    public void AddKill()
    {
        killCount++;
        UIManager.Instance.UpdateKillCount(killCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(10f); // Ajuste o valor do dano conforme necessário
        }
    }

    public void ResetShootAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Shoot", false);
        }
    }
}
