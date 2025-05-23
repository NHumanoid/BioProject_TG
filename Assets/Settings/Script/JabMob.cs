using TanMak;
using UnityEngine;

namespace TanMak 
{
    public class JabMob : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float health;
        public Sprite[] sprites;

        SpriteRenderer spriteRenderer;
        Rigidbody2D rigid;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();
            rigid.linearVelocity = Vector2.down * speed;
        }

        public void OnHit(int dmg)
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);

            health -= dmg;
            if (health <= 0) { Destroy(this.gameObject); }
        }

        void ReturnSprite()
        {
            spriteRenderer.sprite = sprites[0];
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Border")
            {
                Destroy(collision.gameObject); 
            }
            if (collision.gameObject.tag == "PlayerBullet") 
            { 
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                OnHit(bullet.dmg);

                Destroy(collision.gameObject);
            }
            
        }
    }
}
