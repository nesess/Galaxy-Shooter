using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    [SerializeField]
    private float _powerUpSpeed = 3.0f;
    private int choice;
    private Player player;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>(); // player atamak için bu lazım

        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, -1 * _powerUpSpeed * Time.deltaTime, 0);

        if (transform.position.y < -6.4)
        {
            Destroy(this.gameObject);
        }
        
        if (player.getLife() < 1)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                if (this.gameObject.tag == "TripleShot")
                    choice = 0;
                else if (this.gameObject.tag == "Speed")
                    choice = 1;
                else if (this.gameObject.tag == "Shield")
                    choice = 2;
                
                player.enablePowerUp(choice);
            }
            Destroy(this.gameObject);
        }
       
    }
}


