using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float speed;
    private int count;
    private float moveHorizontal;
    private float moveVertical;
    public int health;
    public GUIText countText;
    public int power;
    public GUIText healthText;
    private Vector3 movement;

    void Start()
    {
        count = 0;
        power = 5;
        health = 100;
        setCountText();
        setHealthText();
    }
    void FixedUpdate()
    {
        
        
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.AddForce(movement * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = new Vector3(0.0f, 3f, 0.0f) + movement;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
            power += 2;
        }
        if (other.gameObject.tag == "Trampoline")
        {
            rigidbody.velocity = movement + new Vector3(0.0f, 10f, 0.0f);
        }
        
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyControl enemy = (EnemyControl)other.collider.GetComponent("EnemyControl");
            enemy.health -= power;
            setHealthText();
            if (enemy.health < 1)
            {
                enemy.healthText.text = " ";
                enemy.gameObject.SetActive(false);
            }
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void setHealthText()
    {
        healthText.text = "Player Health: " + health.ToString();
    }
}
