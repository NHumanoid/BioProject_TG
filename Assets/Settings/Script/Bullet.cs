using UnityEngine;

namespace TanMak
{
    public class Bullet : MonoBehaviour
    {
        private void Awake()
        {
            OnTriggerEnter2D(GetComponent<Collider2D>());
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "BulletBorder")
            {
                Destroy(gameObject);
            }
        }
    }
}
