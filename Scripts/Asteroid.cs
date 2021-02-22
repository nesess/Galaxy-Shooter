using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private int _RotateSpeed = 200;
    [SerializeField]
    private  int _Speed = 3;
    private Player player;

    [SerializeField]
    private GameObject _explosionPrefab;
    private Vector3 myPos;
    private int side;

    private CircleCollider2D collider;

    AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        sideChoser();
        collider = GetComponent<CircleCollider2D>();

        explosionSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();
        killMe();

    }

    void killMe()
    {
        if (player.getLife() < 1)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.y < -6.4 || transform.position.y > 9 )
        {
            Destroy(this.gameObject);
        }

        if (transform.position.x < -12 || transform.position.x > 12)
            Destroy(this.gameObject);
    }

    private void Movement()
    {
        switch(side)
        {
            case 0:
                transform.position += new Vector3(1 * _Speed * Time.deltaTime, -1 * _Speed * Time.deltaTime, 0);
                break;
            case 1:
                transform.position += new Vector3(-1 * _Speed * Time.deltaTime, -1 * _Speed * Time.deltaTime, 0);
                break;
            case 2:
                transform.position += new Vector3(-1 * _Speed * Time.deltaTime, -1 * _Speed * Time.deltaTime, 0);
                break;
            case 3:
                transform.position += new Vector3(-1 * _Speed * Time.deltaTime, 1 * _Speed * Time.deltaTime, 0);
                break;
            case 4:
                transform.position += new Vector3(-1 * _Speed * Time.deltaTime, 1 * _Speed * Time.deltaTime, 0);
                break;
            case 5:
                transform.position += new Vector3(1 * _Speed * Time.deltaTime, 1 * _Speed * Time.deltaTime, 0);
                break;
            case 6:
                transform.position += new Vector3(1 * _Speed * Time.deltaTime, 1 * _Speed * Time.deltaTime, 0);
                break;
            case 7:
                transform.position += new Vector3(1 * _Speed * Time.deltaTime, -1 * _Speed * Time.deltaTime, 0);
                break;
        }
        transform.Rotate(0, 0, _RotateSpeed * Time.deltaTime);
    }

    private void sideChoser()
    {
        myPos = new Vector3(transform.position.x, transform.position.y, 0);
        if (myPos.y < 7.9 && myPos.y > 7.6)
        {
            if (myPos.x <= 0)
                side = 0;
            else
                side = 1;
        }
        else if (myPos.x < 11.1 && myPos.x > 10.9)
        {
            if (myPos.y >= 1)
                side = 2;
            else
                side = 3;
        }
        else if (myPos.y < -5.9 && myPos.y > -6.1)
        {
            if (myPos.x >= 0)
                side = 4;
            else
                side = 5;
        }
        else if (myPos.x < -10.9 && myPos.x > -11.1)
        {
            if (myPos.y <= 1)
                side = 6;
            else
                side = 7;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            explosionSound.Play();
            Destroy(collider);
            _Speed = 0;
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
              
            }
            Vector3 AsteroidPos = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(_explosionPrefab, AsteroidPos, Quaternion.identity);
            
            Destroy(this.gameObject,1.5f);
        }
        else if (other.gameObject.tag == "Laser")
        {
            explosionSound.Play();
            Destroy(collider);
            _Speed = 0;
            player.increaseScore5x();
            Destroy(other.gameObject);
            Vector3 AsteroidPos = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(_explosionPrefab, AsteroidPos, Quaternion.identity);
            Destroy(this.gameObject,1.5f);
        }
    }

}
