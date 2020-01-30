using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float speed = 175.0f;
    private float zBound = 6;
    private Rigidbody playerRb;
    private int count;
    public Text countText;
    public Text gameoverText;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        gameoverText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        MovePLayer();
        ConstrainPlayerPosition();
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }

    void GameoverText()
    {
        gameoverText.text = "GAME OVER, YOU LOSE!";
    }

    void GameoverDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MovePLayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

    }
    void ConstrainPlayerPosition()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with an enemy, Game Over.. RESTARTING...");
            GameoverText();
            Invoke("GameoverDelay", 4);

        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
}   

