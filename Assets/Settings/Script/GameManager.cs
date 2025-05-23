using UnityEngine;

namespace TanMak
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] enemyObject;
        public Transform[] spawnPoints;

        [SerializeField]
        private float maxSpawnDelay;
        [SerializeField]
        private float curSpawnDelay;
        [SerializeField]
        private float speed;


        private void Update()
        {
            curSpawnDelay += Time.deltaTime; //���� �߻� �ӵ��� �����մϴ�.
            if (curSpawnDelay > maxSpawnDelay) 
            {
                SpawnEnemy();
                maxSpawnDelay = Random.Range(0.5f, 3f);
                curSpawnDelay = 0f;
            }
        }

        void SpawnEnemy()
        {
            int ranEnemy = Random.Range(0,9);
            int ranPoint = Random.Range(0,9);
            GameObject enemy = Instantiate(enemyObject[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
            
            JabMob jabMob = enemy.GetComponent<JabMob>();
            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
            
            if (ranPoint == 5 || ranPoint == 6)
            {
                rigid.linearVelocity = new Vector2(1*speed, 0.25f);
                enemy.transform.Rotate(Vector3.back*90);
            }
            else if (ranPoint == 7 || ranPoint == 8) 
            {
                rigid.linearVelocity = new Vector2(-1*speed, 0.25f);
                enemy.transform.Rotate(Vector3.forward * 90);
            }
            else 
            {
                rigid.linearVelocity = new Vector2(0, speed);
            }
        }
    }
}
