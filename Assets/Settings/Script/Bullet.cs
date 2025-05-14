using UnityEngine;

namespace TanMak
{
    public class Bullet : MonoBehaviour
    {
        public int dmg;
        private void Awake()
        {
            OnTriggerEnter2D(GetComponent<Collider2D>());
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Border")
            {
                Destroy(gameObject);
            }
            /*if (collision.gameObject.tag == "JabMob")
            {
                Destroy(gameObject);
            }*/
        }
    }
}
