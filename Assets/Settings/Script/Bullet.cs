using UnityEngine;

namespace TanMak
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "BulletBorder")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
