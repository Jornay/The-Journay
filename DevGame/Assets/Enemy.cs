using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour 
{
    //public Animator enemyAnim;
    public float damageTime = 1f;
    public int health;
    private bool invulnerable = false;

    public Transform healthBar;
    public Slider healthFill;

    public float maxHealth;
    public float healthBarYOffset = 1f;

    void Start()
    {
        //enemyAnim.enabled = true;
    }
 
    void Update () 
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            if (gameObject.tag == "BOSS")
            {
                Destroy (GameObject.FindWithTag("BOSSHealthSystem"));
            }
        }
        if (gameObject.tag == "BOSS")
        {
            PositionHealthBar();
            ChangeHealth();
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
            if (collision.transform.tag == "Player")
            {
                invulnerable = false;
                //Debug.Log("Damage given");
                //enemyAnim.SetBool("attack", true);
                collision.GetComponent<HealthSystem>().Damaged();
                //StartCoroutine(GodMode());
                //enemyAnim.SetBool("attack", false);
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
        Vector3 currentPos = GameObject.FindGameObjectWithTag("BOSS").transform.position;

        healthBar.position = new Vector3(currentPos.x, currentPos.y + healthBarYOffset, currentPos.z);
    }
}
