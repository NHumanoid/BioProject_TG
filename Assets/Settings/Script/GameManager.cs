using UnityEngine;

namespace TanMak
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] enemyObject; //�� ������ �迭
        public Transform[] spawnPoints; //�� ���� ����Ʈ �迭
        
        [SerializeField]
        private float speed = 5f; //���� �̵� �ӵ�

        /// �� ���� ������
        [SerializeField]
        private float maxSpawnDelay;
        private float curSpawnDelay;

        void Update()
        {

            curSpawnDelay += Time.deltaTime; // ���� ���� �����̸� ������ŵ�ϴ�.

            if (curSpawnDelay > maxSpawnDelay) 
            {
                SpawnEnemy();
                maxSpawnDelay = Random.Range(0.5f, 3f); // ���� ���� �����̸� �������� �����մϴ�.
                curSpawnDelay = 0f;
            }
        }

        void SpawnEnemy()
        {

            int ranEnemy = Random.Range(0,2); //�������� ���� �����մϴ�. 0�� JabMob, 1�� JabMob2�Դϴ�.
            int ranPoint = Random.Range(0,9); //�������� ���� ����Ʈ�� �����մϴ�.

            GameObject enemy = Instantiate(enemyObject[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>(); //���� ������ٵ� �����ɴϴ�.

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
