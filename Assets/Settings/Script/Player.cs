using Unity.VisualScripting;
using UnityEngine;

namespace TanMak 
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f; //�÷��̾��� �̵� �ӵ��� �����մϴ�.
        [SerializeField]
        private float power; //�߻� �ӵ��� �����մϴ�.
        [SerializeField]
        private float curShotDelay; //���� �߻� �ӵ��� �����մϴ�.
        [SerializeField]
        private GameObject playerBullet; //�Ѿ��� �����մϴ�.
        [SerializeField]
        private float maxShotDelay ; //�ִ� �߻� �ӵ��� �����մϴ�.

        #region Touch
        //���� Ʈ���ſ� ������ true�� �����Ͽ� �̵��� �����մϴ�.
        private bool isTouchTop;
        private bool isTouchBottom;
        private bool isTouchLeft;
        private bool isTouchRight;
        #endregion

        Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>(); //�ִϸ��̼��� �����ɴϴ�.
        }

        private void Update()
        {
            GroundNotOut(); //ȭ�� ������ ������ �ʵ��� �մϴ�.
            Fire(); //�Ѿ��� �߻��մϴ�.
            Move(); //�̵��� �մϴ�.
            Reload(); //�߻� �ӵ��� �����մϴ�.
        }

        private void Fire() 
        {

            if (Input.GetButton("Fire1")) { Instantiate(playerBullet); } //Fire1 ��ư�� ������ ��
            //if (curShotDelay < maxShotDelay) { return; } //�߻� �ӵ��� �����մϴ�.

            switch (power)
            {
                case 1:
                    GameObject bullet = Instantiate(playerBullet, transform.position, transform.rotation);
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    rigid.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //�Ѿ��� �߻��մϴ�.
                    
                    curShotDelay = 0f; //�߻� �ӵ��� �ʱ�ȭ�մϴ�.
                    Destroy(playerBullet, 1f); //�Ѿ��� 2�� �Ŀ� �ı��մϴ�.
                    break;
                case 2:
                    GameObject bulletL = Instantiate(playerBullet, transform.position + Vector3.left * 0.25f, transform.rotation);
                    GameObject bulletR = Instantiate(playerBullet, transform.position + Vector3.right * 0.25f, transform.rotation);
                    Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                    rigidR.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //�Ѿ��� �߻��մϴ�.
                    rigidL.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //�Ѿ��� �߻��մϴ�.

                    curShotDelay = 0f; //�߻� �ӵ��� �ʱ�ȭ�մϴ�.
                    Destroy(playerBullet, 1f); //�Ѿ��� 2�� �Ŀ� �ı��մϴ�.
                    break;

                    
            }
            curShotDelay = 0f; //�߻� �ӵ��� �ʱ�ȭ�մϴ�.
            Destroy(playerBullet, 1f); //�Ѿ��� 2�� �Ŀ� �ı��մϴ�.
        }

        private void Move() 
        {
            float h = Input.GetAxisRaw("Horizontal"); //�¿�� �̵��� �մϴ�.
            if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) { h = 0f; } //�¿�� �̵��� �����մϴ�.

            float v = Input.GetAxisRaw("Vertical"); //���Ϸ� �̵��� �մϴ�.
            if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) { v = 0f; } //�¿�� �̵��� �����մϴ�.

            Vector3 curPos = this.transform.position; //���� ��ġ�� �����ɴϴ�.
            Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //�̵��� ������ �����մϴ�.

            this.transform.position = curPos + nextPos; //���� ��ġ�� �̵��� ������ ���մϴ�.
            /*if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            {
                anim.GetInteger("input");
            }*/
        }

        void Reload() 
        {
            curShotDelay += Time.deltaTime; //�߻� �ӵ��� �����մϴ�.
        }

        private void GroundNotOut()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.x < 0f) pos.x = 0f;
            if (pos.x > 1f) pos.x = 1f; 
            if (pos.y < 0f) pos.y = 0f; 
            if (pos.y > 1f) pos.y = 1f;
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
        #region Trigger
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Border") 
            {
                switch (collision.gameObject.name)
                {
                    case "Top":
                        isTouchTop = true;
                        break;
                    case "Bottom":
                        isTouchBottom = true;
                        break;
                    case "Left":
                        isTouchLeft = true;
                        break;
                    case "Right":
                        isTouchRight = true;
                        break;
                }
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Border")
            {
                switch (collision.gameObject.name)
                {
                    case "Top":
                        isTouchTop = false;
                        break;
                    case "Bottom":
                        isTouchBottom = false;
                        break;
                    case "Left":
                        isTouchLeft = false;
                        break;
                    case "Right":
                        isTouchRight = false;
                        break;
                }
            }
        }
        #endregion
    }
}
