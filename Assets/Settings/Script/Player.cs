using Unity.VisualScripting;
using UnityEngine;

namespace TanMak 
{
    public class Player : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float speed = 5f; //플레이어의 이동 속도를 설정합니다.
        [SerializeField]
        private float power; //발사 힘을 설정합니다.
        [SerializeField]
        private float curShotDelay; //현재 발사 속도를 설정합니다.
        [SerializeField]
        private GameObject playerBullet; //총알을 설정합니다. (총알에 Is treger설정 필수)
        [SerializeField]
        private float maxShotDelay ; //최대 발사 속도를 설정합니다.
        #endregion

        #region Touch 위에 트리거에 닿으면 이동을 제한합니다.
        //위에 트리거에 닿으면 true로 설정하여 이동을 제한합니다.
        /*private bool isTouchTop;
        private bool isTouchBottom;
        private bool isTouchLeft;
        private bool isTouchRight;*/
        #endregion

        private Animator anim;

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

            if (!Input.GetButton("Fire1")) // 발사버튼을 누르지 않으면 발사하지 않습니다.
            {
                return; //발사를 허용하지 않기위하여 리턴합니다.
            }
             
            if (curShotDelay < maxShotDelay)
            {
                return;  //발사 속도를 설정합니다.
            }

            //아래의 switch문은 발사량을 설정합니다. 만약에 발사 량이 1이라면 총알을 하나 발사합니다. 그리고 발사 속도를 초기화합니다.
            //그리고 만약에 발사량이 2라면 총알을 두개 발사합니다. 그리고 발사 속도를 초기화합니다.

            switch (power) //발사의 힘(량)을 설정합니다.
            {
                case 1: //만약에 발사량이 1이라면
                    // 게임 오브젝트 bullet 지역변수를 선언 하여 Instantiate함수로 집어 넣습니다. Instantiate(생성할 오브젝트, 생성할 위치, 회전값);
                    GameObject bullet = Instantiate(playerBullet, transform.position, transform.rotation); //총알을 생성합니다.
                    //아랫 줄은 리지드바디 rigid 변수를 생성하여 총알의 리지드 바디를 가져옵니다.
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); // 총알의 Rigidbody2D를 가져옵니다.
                    //아랫 줄은 rigid 변수를 사용하여 AddForce함수로 총알을 발사합니다. AddForce(발사할 힘, 발사할 힘의 모드);
                    rigid.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); //총알을 위로(Vector2.up) 발사합니다.

                    //addForce(힘을 더하다) : Rigidbody2D에 힘을 더합니다.
                    //참고 : ForMode2D.Impulse : 순간적으로 힘을 가합니다. (즉각적인 힘을 가합니다.)

                    // 총알을 다 쏘았으면 발사 속도를 초기화합니다. (아렛 줄)
                    curShotDelay = 0f; //발사 속도를 초기화합니다.
                    break;

                case 2: //만약에 발사량이 2라면 좌우의 총알을 발사합니다.
                    GameObject bulletL = Instantiate(playerBullet, transform.position + Vector3.left * 0.25f, transform.rotation);
                    GameObject bulletR = Instantiate(playerBullet, transform.position + Vector3.right * 0.25f, transform.rotation);
                    Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                    rigidR.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                    rigidL.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); 
                    
                    curShotDelay = 0f; //발사 속도를 초기화합니다.
                    break; //break문을 사용하여 switch문을 종료합니다. (참고)
            }
            
        }

        private void Move() 
        {
            //참조 : https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html
            // Input.GetAxisRaw("Horizontal") : -1(왼쪽) ~ 0(정지) ~ 1(오른쪽)으로 이동합니다.
            // Input.GetAxisRaw("Vertical") : -1(아래) ~ 0(정지) ~ 1(위쪽)으로 이동합니다.

            float h = Input.GetAxisRaw("Horizontal"); //좌우로 이동을 합니다.
            //if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) { h = 0f; } //좌우로 이동을 제한합니다.

            float v = Input.GetAxisRaw("Vertical"); //상하로 이동을 합니다.
            //if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) { v = 0f; } //좌우로 이동을 제한합니다.

            Vector3 curPos = this.transform.position; //현재 위치를 가져옵니다.
            Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; //이동할 방향을 설정합니다.
            // transform 이동에는 Time.deltaTime을 곱해줘야 합니다. (프레임에 따라 이동 속도가 달라지기 때문입니다.)

            this.transform.position = curPos + nextPos; //현재 위치에 이동할 방향을 더합니다.
            //실질적 이동이 구현하는 줄입니다,

            /*if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            {
                anim.GetInteger("input");
            }*/
        }

        void Reload() 
        {
            curShotDelay += Time.deltaTime; //발사 속도를 설정합니다.
        }

        #region 트러거로 플레이어를 밖에 나가지 않도록 사용하기
        // 만약 트리거로 Player를 밖에 나가지 않도록 사용할때 아래의 주석을 해제합니다.

        //만약에 Rigidbody2D를 사용한다면 타입은 정적(static)으로 설정해야합니다.
        //트리거에 닿으면 실행하는 함수입니다.
        //참조 : https://docs.unity3d.com/ScriptReference/Collider2D.OnTriggerEnter2D.html

        /*private void OnTriggerEnter2D(Collider2D collision)
        {
            //만약에 Border테그의 트리거에 닿으면 실행합니다.
            if ( collision.gameObject.tag == "Border")
            {
                //collision.gameObject.name : Border의 이름을 가져옵니다.
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
