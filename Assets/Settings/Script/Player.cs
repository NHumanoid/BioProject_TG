using Unity.VisualScripting;
using UnityEngine;

namespace TanMak 
{
    public class Player : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float speed = 5f; //�÷��̾��� �̵� �ӵ��� �����մϴ�.
        [SerializeField]
        private float power; //�߻� ���� �����մϴ�.
        [SerializeField]
        private float curShotDelay; //���� �߻� �ӵ��� �����մϴ�.
        [SerializeField]
        private GameObject playerBullet; //�Ѿ��� �����մϴ�. (�Ѿ˿� Is treger���� �ʼ�)
        [SerializeField]
        private float maxShotDelay ; //�ִ� �߻� �ӵ��� �����մϴ�.
        #endregion

        #region Touch ���� Ʈ���ſ� ������ �̵��� �����մϴ�.
        //���� Ʈ���ſ� ������ true�� �����Ͽ� �̵��� �����մϴ�.
        /*private bool isTouchTop;
        private bool isTouchBottom;
        private bool isTouchLeft;
        private bool isTouchRight;*/
        #endregion

        private Animator anim;

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

            if (!Input.GetButton("Fire1")) // �߻��ư�� ������ ������ �߻����� �ʽ��ϴ�.
            {
                return; //�߻縦 ������� �ʱ����Ͽ� �����մϴ�.
            }
             
            if (curShotDelay < maxShotDelay)
            {
                return;  //�߻� �ӵ��� �����մϴ�.
            }

            //�Ʒ��� switch���� �߻緮�� �����մϴ�. ���࿡ �߻� ���� 1�̶�� �Ѿ��� �ϳ� �߻��մϴ�. �׸��� �߻� �ӵ��� �ʱ�ȭ�մϴ�.
            //�׸��� ���࿡ �߻緮�� 2��� �Ѿ��� �ΰ� �߻��մϴ�. �׸��� �߻� �ӵ��� �ʱ�ȭ�մϴ�.

            switch (power) //�߻��� ��(��)�� �����մϴ�.
            {
                case 1: //���࿡ �߻緮�� 1�̶��
                    // ���� ������Ʈ bullet ���������� ���� �Ͽ� Instantiate�Լ��� ���� �ֽ��ϴ�. Instantiate(������ ������Ʈ, ������ ��ġ, ȸ����);
                    GameObject bullet = Instantiate(playerBullet, transform.position, transform.rotation); //�Ѿ��� �����մϴ�.
                    //�Ʒ� ���� ������ٵ� rigid ������ �����Ͽ� �Ѿ��� ������ �ٵ� �����ɴϴ�.
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); // �Ѿ��� Rigidbody2D�� �����ɴϴ�.
                    //�Ʒ� ���� rigid ������ ����Ͽ� AddForce�Լ��� �Ѿ��� �߻��մϴ�. AddForce(�߻��� ��, �߻��� ���� ���);
                    rigid.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //�Ѿ��� ����(Vector2.up) �߻��մϴ�.

                    //addForce(���� ���ϴ�) : Rigidbody2D�� ���� ���մϴ�.
                    //���� : ForMode2D.Impulse : ���������� ���� ���մϴ�. (�ﰢ���� ���� ���մϴ�.)

                    // �Ѿ��� �� ������� �߻� �ӵ��� �ʱ�ȭ�մϴ�. (�Ʒ� ��)
                    curShotDelay = 0f; //�߻� �ӵ��� �ʱ�ȭ�մϴ�.
                    break;

                case 2: //���࿡ �߻緮�� 2��� �¿��� �Ѿ��� �߻��մϴ�.
                    GameObject bulletL = Instantiate(playerBullet, transform.position + Vector3.left * 0.25f, transform.rotation);
                    GameObject bulletR = Instantiate(playerBullet, transform.position + Vector3.right * 0.25f, transform.rotation);
                    Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                    rigidR.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                    rigidL.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); 
                    
                    curShotDelay = 0f; //�߻� �ӵ��� �ʱ�ȭ�մϴ�.
                    break; //break���� ����Ͽ� switch���� �����մϴ�. (����)
            }
            
        }

        private void Move() 
        {
            //���� : https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html
            // Input.GetAxisRaw("Horizontal") : -1(����) ~ 0(����) ~ 1(������)���� �̵��մϴ�.
            // Input.GetAxisRaw("Vertical") : -1(�Ʒ�) ~ 0(����) ~ 1(����)���� �̵��մϴ�.

            float h = Input.GetAxisRaw("Horizontal"); //�¿�� �̵��� �մϴ�.
            //if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) { h = 0f; } //�¿�� �̵��� �����մϴ�.

            float v = Input.GetAxisRaw("Vertical"); //���Ϸ� �̵��� �մϴ�.
            //if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) { v = 0f; } //�¿�� �̵��� �����մϴ�.

            Vector3 curPos = this.transform.position; //���� ��ġ�� �����ɴϴ�.
            Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //�̵��� ������ �����մϴ�.
            // transform �̵����� Time.deltaTime�� ������� �մϴ�. (�����ӿ� ���� �̵� �ӵ��� �޶����� �����Դϴ�.)

            this.transform.position = curPos + nextPos; //���� ��ġ�� �̵��� ������ ���մϴ�.
            //������ �̵��� �����ϴ� ���Դϴ�,

            /*if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            {
                anim.GetInteger("input");
            }*/
        }

        void Reload() 
        {
            curShotDelay += Time.deltaTime; //�߻� �ӵ��� �����մϴ�.
        }

        #region Ʈ���ŷ� �÷��̾ �ۿ� ������ �ʵ��� ����ϱ�
        // ���� Ʈ���ŷ� Player�� �ۿ� ������ �ʵ��� ����Ҷ� �Ʒ��� �ּ��� �����մϴ�.

        //���࿡ Rigidbody2D�� ����Ѵٸ� Ÿ���� ����(static)���� �����ؾ��մϴ�.
        //Ʈ���ſ� ������ �����ϴ� �Լ��Դϴ�.
        //���� : https://docs.unity3d.com/ScriptReference/Collider2D.OnTriggerEnter2D.html

        /*private void OnTriggerEnter2D(Collider2D collision)
        {
            //���࿡ Border�ױ��� Ʈ���ſ� ������ �����մϴ�.
            if ( collision.gameObject.tag == "Border")
            {
                //collision.gameObject.name : Border�� �̸��� �����ɴϴ�.
                switch (collision.gameObject.name)
                {
                    case "Top":
                        //isTouchTop = true;
                        break;
                    case "Bottom":
                        //isTouchBottom = true;
                        break;
                    case "Left":
                        //isTouchLeft = true;
                        break;
                    case "Right":
                        //isTouchRight = true;
                        break;
                }
            }
        }*/
        #endregion

        private void GroundNotOut()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.x < 0f) pos.x = 0f;
            if (pos.x > 1f) pos.x = 1f; 
            if (pos.y < 0f) pos.y = 0f; 
            if (pos.y > 1f) pos.y = 1f;
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }
}
