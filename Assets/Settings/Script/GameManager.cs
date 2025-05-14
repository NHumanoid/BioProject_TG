using UnityEngine;

namespace TanMak
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] enemyObject; //적 프리펩 배열
        public Transform[] spawnPoints; //적 스폰 포인트 배열
        
        [SerializeField]
        private float speed = 5f; //적의 이동 속도

        /// 적 스폰 딜레이
        [SerializeField]
        private float maxSpawnDelay;
        private float curSpawnDelay;

        void Update()
        {

            curSpawnDelay += Time.deltaTime; // 현재 스폰 딜레이를 증가시킵니다.

            if (curSpawnDelay > maxSpawnDelay) 
            {
                SpawnEnemy();
                maxSpawnDelay = Random.Range(0.5f, 3f); // 다음 스폰 딜레이를 랜덤으로 설정합니다.
                curSpawnDelay = 0f;
            }
        }

        void SpawnEnemy()
        {

            int ranEnemy = Random.Range(0,2); //랜덤으로 적을 선택합니다. 0은 JabMob, 1은 JabMob2입니다.
            int ranPoint = Random.Range(0,9); //랜덤으로 스폰 포인트를 선택합니다.

            GameObject enemy = Instantiate(enemyObject[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>(); //적의 리지드바디를 가져옵니다.

            if (ranPoint == 5 || ranPoint == 6 )
            {
            }
            if (ranPoint <= 6)
            {
                enemyObject[ranEnemy].transform.position += Vector3.left * Time.deltaTime * speed;
            }
            if (ranPoint > 6)
                enemyObject[ranEnemy].transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }
}
