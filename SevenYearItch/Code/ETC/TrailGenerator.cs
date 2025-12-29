using UnityEngine;

namespace _00Work.LKW.Code.ETC
{
    public class TrailGenerator : MonoBehaviour
    {
        [SerializeField] private Transform visual;
        [SerializeField] private Trail trailPrefab;
        [SerializeField] private float spawnDelay = 0.04f;

        private float timer = 0f;
        private bool isMoving = true;

        private void Start()
        {
            isMoving = true;
        }

        private void Update()
        {
            if (!isMoving) return;

            timer += Time.deltaTime;

            if (timer >= spawnDelay)
            {
                timer = 0f;
                SpawnTrail();
            }
        }

        private void SpawnTrail()
        {
            Vector3 direction = visual.up;
            Vector3 spawnOffset = -direction;
            Vector3 spawnPosition = visual.position + spawnOffset;

            Trail trail = Instantiate(trailPrefab, spawnPosition, Quaternion.identity);
            trail.transform.up = direction;
        }
    }
}