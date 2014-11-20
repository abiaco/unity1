using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
    public float speed;
    public int power;
    public int health;
    public GUIText healthText;
	// Use this for initialization
	void Start () {
        power = 10;
        health = 100;
        setHealthText();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveX = Random.value;
        float moveY = Random.value;
        Vector3 move = new Vector3(moveX, 0.0f, moveY);
        rigidbody.AddForce(move * speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerControl player = (PlayerControl) other.GetComponent("PlayerControl");
            health -= player.power;
            setHealthText();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerControl player = (PlayerControl)other.collider.GetComponent("PlayerControl");
            player.health -= power;
            setHealthText();
            if (player.health < 1)
            {
                player.healthText.text = " ";
                player.gameObject.SetActive(false);
                
            }
        }
    }
    void setHealthText()
    {
        healthText.text = "Enemy Health: " + health.ToString();
    }
}
