using Unity.VisualScripting;
using UnityEngine;

namespace TanMak 
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f; //플레이어의 이동 속도를 설정합니다.
        [SerializeField]
        private float power; //발사 속도를 설정합니다.
        [SerializeField]
        private float curShotDelay; //현재 발사 속도를 설정합니다.
        [SerializeField]
        private GameObject playerBullet; //총알을 설정합니다.
        [SerializeField]
        private float maxShotDelay ; //최대 발사 속도를 설정합니다.

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
            GroundNotOut(); //화면 밖으로 나가지 않도록 합니다.
            Fire(); //총알을 발사합니다.
            Move(); //이동을 합니다.
            Reload(); //발사 속도를 설정합니다.
        }

        private void Fire() 
        {

            if (Input.GetButton("Fire1")) { Instantiate(playerBullet); } //Fire1 버튼을 눌렀을 때
            //if (curShotDelay < maxShotDelay) { return; } //발사 속도를 설정합니다.

            switch (power)
            {
                case 1:
                    GameObject bullet = Instantiate(playerBullet, transform.position, transform.rotation);
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    rigid.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //총알을 발사합니다.
                    
                    curShotDelay = 0f; //발사 속도를 초기화합니다.
                    Destroy(playerBullet, 1f); //총알을 2초 후에 파괴합니다.
                    break;
                case 2:
                    GameObject bulletL = Instantiate(playerBullet, transform.position + Vector3.left * 0.25f, transform.rotation);
                    GameObject bulletR = Instantiate(playerBullet, transform.position + Vector3.right * 0.25f, transform.rotation);
                    Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                    rigidR.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //총알을 발사합니다.
                    rigidL.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //총알을 발사합니다.

                    curShotDelay = 0f; //발사 속도를 초기화합니다.
                    Destroy(playerBullet, 1f); //총알을 2초 후에 파괴합니다.
                    break;

                    
            }
            curShotDelay = 0f; //발사 속도를 초기화합니다.
            Destroy(playerBullet, 1f); //총알을 2초 후에 파괴합니다.
        }

        private void Move() 
        {
            float h = Input.GetAxisRaw("Horizontal"); //좌우로 이동을 합니다.
            if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) { h = 0f; } //좌우로 이동을 제한합니다.

            float v = Input.GetAxisRaw("Vertical"); //상하로 이동을 합니다.
            if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) { v = 0f; } //좌우로 이동을 제한합니다.

            Vector3 curPos = this.transform.position; //현재 위치를 가져옵니다.
            Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //이동할 방향을 설정합니다.

            this.transform.position = curPos + nextPos; //현재 위치에 이동할 방향을 더합니다.
            /*if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            {
                anim.GetInteger("input");
            }*/
        }

        void Reload() 
        {
            curShotDelay += Time.deltaTime; //발사 속도를 설정합니다.
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
