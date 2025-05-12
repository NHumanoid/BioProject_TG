using UnityEngine;

namespace TanMak
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] enemyObject;
        public Transform[] spawnPoints;

        public float maxSpawnDelay;
        public float curSpawnDelay;

        private void Update()
        {
            if (curSpawnDelay > maxSpawnDelay) 
            {
                SpawnEnemy();
                maxSpawnDelay = Random.Range(0.5f, 3f);
                curSpawnDelay = 0f;
            }
        }

        void SpawnEnemy()
        {
            int ranEnemy = Random.Range(0,3);
            int ranPoint = Random.Range(0,3);
            Instantiate(enemyObject[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        }
    }
}
