using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 12;

    void Update()
    {
        transform.Translate(0, 1* _laserSpeed * Time.deltaTime, 0);
        if(transform.position.y > 8)
        {
            if(transform.parent!=null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
