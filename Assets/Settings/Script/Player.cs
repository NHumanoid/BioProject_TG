using UnityEngine;

namespace TanMak 
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
         private float speed = 5f; //�÷��̾��� �̵� �ӵ��� �����մϴ�.

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
            float h = Input.GetAxisRaw("Horizontal"); //�¿�� �̵��� �մϴ�.
            if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) { h = 0f; } //�¿�� �̵��� �����մϴ�.

            float v = Input.GetAxisRaw("Vertical"); //���Ϸ� �̵��� �մϴ�.
            if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) { v = 0f; } //�¿�� �̵��� �����մϴ�.

            Vector3 curPos = this.transform.position; //���� ��ġ�� �����ɴϴ�.
            Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //�̵��� ������ �����մϴ�.

            this.transform.position = curPos + nextPos; //���� ��ġ�� �̵��� ������ ���մϴ�.

            if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            {
                anim.GetInteger("input");
            }
        }

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

        void Trigger() 
        {
        }
    }
}
