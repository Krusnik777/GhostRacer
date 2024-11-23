using Ashsvp;
using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public enum RaceState
    {
        Preparation,
        Countdown,
        Race,
        Passed
    }

    public class RaceStateTracker : MonoBehaviour, IDependency<TrackpointCircuit>, IDependency<SimcadeVehicleController>
    {
        [SerializeField] private Timer m_countdownTimer;
        [SerializeField] private int m_lapsToComplete;

        public event UnityAction EventOnPreparationStarted;
        public event UnityAction EventOnStarted;
        public event UnityAction EventOnCompleted;
        public event UnityAction<TrackPoint> EventOnTrackPointPassed;
        public event UnityAction<int> EventOnLapCompleted;

        private TrackpointCircuit m_trackpointCircuit;
        public void Construct(TrackpointCircuit trackpointCircuit) => m_trackpointCircuit = trackpointCircuit;

        private SimcadeVehicleController m_vehicleController;
        public void Construct(SimcadeVehicleController vehicleController) => m_vehicleController = vehicleController;

        public Timer CountdownTimer => m_countdownTimer;

        private RaceState state;
        public RaceState State => state;

        public int LapsToComplete => m_lapsToComplete;
        public TrackType RaceType => m_trackpointCircuit.Type;
        public int TrackPointsAmount => m_trackpointCircuit.PointsNumber;

        #region Public

        public void LaunchPreparationStart()
        {
            if (state != RaceState.Preparation) return;

            StartState(RaceState.Countdown);

            m_countdownTimer.enabled = true;

            EventOnPreparationStarted?.Invoke();
        }

        #endregion

        #region Private

        private void Start()
        {
            StartState(RaceState.Preparation);

            m_countdownTimer.enabled = false;
            m_countdownTimer.EventOnFinished += OnCountdownTimerFinished;

            m_trackpointCircuit.EventOnTrackPointTriggered += OnTrackPointTriggered;
            m_trackpointCircuit.EventOnLapCompleted += OnLapCompleted;
        }

        private void OnDestroy()
        {
            m_countdownTimer.EventOnFinished -= OnCountdownTimerFinished;

            m_trackpointCircuit.EventOnTrackPointTriggered -= OnTrackPointTriggered;
            m_trackpointCircuit.EventOnLapCompleted -= OnLapCompleted;
        }

        private void OnCountdownTimerFinished()
        {
            StartRace();
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            EventOnTrackPointPassed?.Invoke(trackPoint);
        }

        private void OnLapCompleted(int lapAmount)
        {
            EventOnLapCompleted?.Invoke(lapAmount);

            if (m_trackpointCircuit.Type == TrackType.Sprint)
            {
                CompleteRace();
            }

            if (m_trackpointCircuit.Type == TrackType.Circular)
            {
                if (lapAmount == m_lapsToComplete)
                    CompleteRace();
                else
                    CompleteLap(lapAmount);
            }
        }

        private void StartState(RaceState state)
        {
            this.state = state;
        }

        private void StartRace()
        {
            if (state != RaceState.Countdown) return;

            StartState(RaceState.Race);

            m_vehicleController.CanDrive = true;

            EventOnStarted?.Invoke();
        }

        private void CompleteLap(int lapAmount)
        {
            EventOnLapCompleted?.Invoke(lapAmount);
        }

        private void CompleteRace()
        {
            if (state != RaceState.Race) return;

            StartState(RaceState.Passed);

            m_vehicleController.CanDrive = false;
            m_vehicleController.CanAccelerate = false;

            EventOnCompleted?.Invoke();
        }

        #endregion

    }
}
