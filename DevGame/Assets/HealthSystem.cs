using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
 {
    public Transform healthBar;
    public Slider healthFill;

    public float maxHealth;
    public float healthBarYOffset = 1f;

    public int health;

    void Update()
    {
        if(transform.position.y < -40)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(GameObject.FindWithTag("PlayerHealthBar"));
        }

        PositionHealthBar();
        ChangeHealth();
        if (health <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Destroy (GameObject.FindWithTag("PlayerHealthBar"));
            }
    }
 
    public void Damaged()
    {
        health -= 1;
    }
    
    public void ChangeHealth()
    {
        healthFill.value = health / maxHealth;
    }

    private void PositionHealthBar()
    {  
        Vector3 currentPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        healthBar.position = new Vector3(currentPos.x, currentPos.y + healthBarYOffset, currentPos.z);
    }
}