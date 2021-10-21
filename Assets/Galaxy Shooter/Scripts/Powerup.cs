using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _Clip;


    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (powerupID == 0)
                {
                    player.TripleShotPowerUpOn();
                }
                else if (powerupID == 1)
                {
                    player.SpeedBoostOn();
                }
                else if (powerupID == 2)
                {
                    player.ShieldPowerupOn();
                }
                AudioSource.PlayClipAtPoint(_Clip, Camera.main.transform.position);
                Destroy(this.gameObject);
            }
        }
    }
}
