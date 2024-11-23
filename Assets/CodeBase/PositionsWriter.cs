using Ashsvp;
using System.Collections.Generic;
using UnityEngine;

namespace Racing
{
    public class PositionsWriter : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>, IDependency<SimcadeVehicleController>
    {
        [SerializeField] private float m_timeBetweenWritings;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private SimcadeVehicleController m_vehicleController;
        public void Construct(SimcadeVehicleController vehicleController) => m_vehicleController = vehicleController;

        private RaceResultTime m_raceResultTime;
        public void Construct(RaceResultTime raceResultTime) => m_raceResultTime = raceResultTime;

        [SerializeField] private List<Vector3> positions;
        [SerializeField] private List<float> speeds;

        private bool canWrite;
        private float timer;

        private void Start()
        {
            m_raceStateTracker.EventOnStarted += OnRaceStarted;
            m_raceStateTracker.EventOnTrackPointPassed += OnTrackPointPassed;
            m_raceStateTracker.EventOnCompleted += OnRaceEnded;

            positions = new List<Vector3>();
            speeds = new List<float>();
            canWrite = false;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
            m_raceStateTracker.EventOnTrackPointPassed -= OnTrackPointPassed;
            m_raceStateTracker.EventOnCompleted -= OnRaceEnded;
        }

        private void Update()
        {
            if (!canWrite) return;

            if (timer >= m_timeBetweenWritings)
            {
                timer = 0;
                positions.Add(m_vehicleController.transform.position);
                speeds.Add(m_vehicleController.CurrentSpeed);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        private void OnRaceStarted()
        {
            positions.Add(m_vehicleController.transform.position);
            speeds.Add(m_vehicleController.CurrentSpeed);

            timer = 0f;
            canWrite = true;
        }

        private void OnTrackPointPassed(TrackPoint trackPoint)
        {
            positions.Add(m_vehicleController.transform.position);
            speeds.Add(m_vehicleController.CurrentSpeed);
        }

        private void OnRaceEnded()
        {
            canWrite = false;

            if (m_raceResultTime.CurrentTime <= m_raceResultTime.PlayerRecordTime)
            {
                RaceController.RaceData.Positions = positions.ToArray();
                RaceController.RaceData.Speeds = speeds.ToArray();
            }
        }
    }
}
