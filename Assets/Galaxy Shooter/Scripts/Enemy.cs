using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _speed = 5f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    private UIManager _UIManager;
    [SerializeField]
    private AudioClip _Clip;
    // Use this for initialization
    void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            float randomX = Random.Range(-7.74f, 7.74f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if(player.shieldActive == true)
                {
                    player.shieldActive = false;
                    player.ShieldObjectOff();

                }
                else
                {
                    player.health = player.health - 1;
                    _UIManager.UpdateLives(player.health);
                    player.hitCount = player.hitCount + 1;
                    if(player.hitCount == 1)
                    {
                        player.engines[0].SetActive(true);
                    }
                    else if (player.hitCount == 2)
                    {
                        player.engines[1].SetActive(true);
                    }
                }
            }
            GameObject enemyExplosion = Instantiate(_enemyExplosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(enemyExplosion, 2.5f);
            AudioSource.PlayClipAtPoint(_Clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Laser laser = other.GetComponent<Laser>();
            if (laser != null)
            {
                GameObject enemyExplosion = Instantiate(_enemyExplosionPrefab, gameObject.transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 2.5f);
                AudioSource.PlayClipAtPoint(_Clip, Camera.main.transform.position);
                Destroy(this.gameObject);
                laser.laserHit = true;
                _UIManager.UpdateScore();
            }
        }
    }
}
