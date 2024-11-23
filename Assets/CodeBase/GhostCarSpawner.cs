using UnityEngine;

namespace Racing
{
    public class GhostCarSpawner : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private GhostCar m_ghostCarPrefab;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private GhostCar ghostCar;

        private void Start()
        {
            if (RaceController.RaceData.RecordTime == 0)
            {
                enabled = false;
                return;
            }

            m_raceStateTracker.EventOnStarted += OnRaceStarted;
            m_raceStateTracker.EventOnCompleted += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
            m_raceStateTracker.EventOnCompleted -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            var startPos = RaceController.RaceData.Positions[0];
            var startSpeed = RaceController.RaceData.Speeds[0];

            ghostCar = Instantiate(m_ghostCarPrefab, startPos, Quaternion.identity);

            ghostCar.SetSpeed(startSpeed);

            ghostCar.Init(RaceController.RaceData.Positions, RaceController.RaceData.Speeds);
        }

        private void OnRaceCompleted()
        {
            //Destroy(ghostCar);
        }
    }
}
