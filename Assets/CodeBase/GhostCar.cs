using UnityEngine;

namespace Racing
{
    public class GhostCar : MonoBehaviour
    {
        private Vector3[] m_positions;
        private float[] m_speeds;
        private float m_speed;

        private int currentPos;

        private Vector3 headingDirection;
        private void CalculateHeadingDirection(Vector3 targetPosition) => headingDirection = (targetPosition - transform.position).normalized;

        public void SetSpeed(float speed) => m_speed = speed;

        public void Init(Vector3[] positions, float[] speeds)
        {
            m_positions = positions;
            m_speeds = speeds;
        }

        private void Start()
        {
            currentPos = 0;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, m_positions[currentPos], m_speed * Time.deltaTime);

            CalculateHeadingDirection(m_positions[currentPos]);
            transform.forward = headingDirection;

            if (Vector3.Distance(transform.position, m_positions[currentPos]) < 0.05f)
            {
                transform.position = m_positions[currentPos];
                currentPos++;

                if (currentPos < m_positions.Length - 1)
                {
                    m_speed = m_speeds[currentPos];
                }

                if (currentPos >= m_positions.Length)
                {
                    enabled = false;
                    return;
                }
            }
        }
    }
}
