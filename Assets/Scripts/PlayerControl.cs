using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float speed;
    private int count;
    public int health;
    public GUIText countText;
    public int power;
    public GUIText healthText;

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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.AddForce(movement * speed * Time.deltaTime);
        
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
