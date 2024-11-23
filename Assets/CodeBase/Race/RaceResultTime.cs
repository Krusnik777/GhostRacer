using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
    {
        public event UnityAction EventOnUpdatedResults;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private RaceTimeTracker m_raceTimeTracker;
        public void Construct(RaceTimeTracker raceTimeTracker) => m_raceTimeTracker = raceTimeTracker;

        private float playerRecordTime;
        public float PlayerRecordTime => playerRecordTime;
        private float currentTime;
        public float CurrentTime => currentTime;

        public bool RecordWasSet => playerRecordTime != 0;

        #region Private

        private void Start()
        {
            Load();

            m_raceStateTracker.EventOnCompleted += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnCompleted -= OnRaceCompleted;
        }

        private void OnRaceCompleted()
        {
            if (playerRecordTime == 0 || playerRecordTime > m_raceTimeTracker.CurrentTime)
            {
                playerRecordTime = m_raceTimeTracker.CurrentTime;

                Save();
            }

            currentTime = m_raceTimeTracker.CurrentTime;

            EventOnUpdatedResults?.Invoke();
        }

        private void Load()
        {
            playerRecordTime = RaceController.RaceData.RecordTime;
            //playerRecordTime = PlayerPrefs.GetFloat(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + SaveMark, 0f);
        }

        private void Save()
        {
            RaceController.RaceData.RecordTime = playerRecordTime;
            //PlayerPrefs.SetFloat(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
        }

        #endregion
    }
}
