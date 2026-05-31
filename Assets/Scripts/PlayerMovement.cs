using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;



public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 5f;
    Rigidbody2D rb;
    float jumpForce = 5f;
    int maxJumps = 2;
    int jumpsRemaining;

    SpriteRenderer spriteRenderer;

    int totalCollectibles = 2;
    int collectedItems = 0;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI collectiblesText;
    




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;

        messageText.gameObject.SetActive(false);

        collectiblesText.text = "Collect TreeGems: " + collectedItems + "/" + totalCollectibles;

    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
        }
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (messageText.gameObject.activeSelf && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            ShowGameOver();
        }
        if (other.CompareTag("Goal"))
        {
            if (collectedItems >= totalCollectibles)
            {
                ShowWin();
            }
            else
            {
                messageText.gameObject.SetActive(true);

                messageText.text =
                    "Collect All TreeGems!\n\n" +
                    collectedItems + "/" + totalCollectibles;

                Invoke(nameof(HideMessage), 2f);
            }
        }
        if (other.CompareTag("Enemy"))
        {
            ShowGameOver();
        }
        if (other.CompareTag("Collectible"))
        {
            collectedItems++;

            collectiblesText.text = "Collect TreeGems: " + collectedItems + "/" + totalCollectibles;

            Debug.Log("Collected Gem");

            Destroy(other.gameObject);
        }
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowGameOver()
    {
        messageText.gameObject.SetActive(true);
        messageText.text = "GAME OVER\nPress R to Restart";

        Time.timeScale = 0f;
    }

    void ShowWin()
    {
        messageText.gameObject.SetActive(true);
        messageText.text = "YOU WIN!\nPress R to Play Again";

        Time.timeScale = 0f;

    }
    void HideMessage()
    {
        if (Time.timeScale == 1f)
        {
            messageText.gameObject.SetActive(false);
        }
    }
}
