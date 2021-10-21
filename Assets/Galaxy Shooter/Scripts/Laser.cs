using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _LaserSpeed = 10f;

    public bool laserHit = false;
    
	void Start () {
		
	}

    void Update ()
    {
        transform.Translate(Vector3.up * _LaserSpeed * Time.deltaTime);

        if(transform.position.y >= 6f)
        {
            Destroy(this.gameObject);
        }

        if(laserHit == true)
        {
            Destroy(this.gameObject);
        }

	}
}
