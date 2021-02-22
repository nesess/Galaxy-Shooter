using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    
    [SerializeField]
    private float _enemySpeed = 4.0f;
    private Player player;

    private Animator animComp;
    private BoxCollider2D box;

    AudioSource explosionSound;

    void Start()
    {
        animComp = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        box = GetComponent<BoxCollider2D>();

        explosionSound = GetComponent<AudioSource>();

    }

   
    void Update()
    {
        
        transform.Translate(0, -1 * _enemySpeed * Time.deltaTime, 0);
        
        if(transform.position.y < -6.4)
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 9, 0);
        }
        
      
        if(player.getLife() <1)
        {
            Destroy(this.gameObject);
        }
        

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            animComp.SetTrigger("enemyDeath");
            _enemySpeed = 0;
            Destroy(box);
            explosionSound.Play();
            Destroy(this.gameObject, 2.8f);
        }
        else if (other.gameObject.tag == "Laser")
        {
            player.increaseScore();
            Destroy(other.gameObject);
            animComp.SetTrigger("enemyDeath");
            _enemySpeed = 0;
            Destroy(box);
            explosionSound.Play();
            Destroy(this.gameObject,2.8f);
        }
    }
}
