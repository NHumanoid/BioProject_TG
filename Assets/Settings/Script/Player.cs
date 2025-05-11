using UnityEngine;

namespace TanMak 
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
         private float speed = 5f; //플레이어의 이동 속도를 설정합니다.

        #region Touch
        //위에 트리거에 닿으면 true로 설정하여 이동을 제한합니다.
        private bool isTouchTop;
        private bool isTouchBottom;
        private bool isTouchLeft;
        private bool isTouchRight;
        #endregion

        Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>(); //애니메이션을 가져옵니다.
        }

        private void Update()
        {
            float h = Input.GetAxisRaw("Horizontal"); //좌우로 이동을 합니다.
            if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) { h = 0f; } //좌우로 이동을 제한합니다.

            float v = Input.GetAxisRaw("Vertical"); //상하로 이동을 합니다.
            if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) { v = 0f; } //좌우로 이동을 제한합니다.

            Vector3 curPos = this.transform.position; //현재 위치를 가져옵니다.
            Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //이동할 방향을 설정합니다.

            this.transform.position = curPos + nextPos; //현재 위치에 이동할 방향을 더합니다.

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
