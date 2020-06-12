using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour 
{
    public float damageTime = 1f;
    public int health;
    private bool invulnerable = false;

    public Transform healthBar;
    public Slider healthFill;

    public float maxHealth;
    public float healthBarYOffset = 1f;

    void Update () 
    {
        PositionHealthBar();
        ChangeHealth();
        if(health <= 0)
        {
            Destroy(gameObject);
            Destroy (GameObject.FindWithTag("Enemy1HealthBar"));
        }
    }
 
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage Taken");
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invulnerable)
        {
            if (collision.transform.name == "Player")
            {
                Debug.Log("Damage given");
                collision.GetComponent<HealthSystem>().Damaged();
                StartCoroutine(GodMode());
            }
        }
    }

    IEnumerator GodMode()
    {
    invulnerable = true;
    yield return new WaitForSeconds(damageTime);
    invulnerable = false;
    }

    public void ChangeHealth()
    {
        healthFill.value = health / maxHealth;
    }

    private void PositionHealthBar()
    {  
        Vector3 currentPos = GameObject.FindGameObjectWithTag("Enemy1").transform.position;

        healthBar.position = new Vector3(currentPos.x, currentPos.y + healthBarYOffset, currentPos.z);
    }
}
