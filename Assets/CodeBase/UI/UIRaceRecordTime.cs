using TMPro;
using UnityEngine;

namespace Racing
{
    public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
    {
        [SerializeField] private GameObject m_playerRecordObject;
        [SerializeField] private TextMeshProUGUI m_playerRecordTimeText;

        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;

        private RaceResultTime m_raceResultTime;
        public void Construct(RaceResultTime raceResultTime) => m_raceResultTime = raceResultTime;

        private void Start()
        {
            m_raceStateTracker.EventOnStarted += OnRaceStarted;
            m_raceStateTracker.EventOnCompleted += OnRaceCompleted;

            m_playerRecordObject.SetActive(false);
        }

        private void OnDestroy()
        {
            m_raceStateTracker.EventOnStarted -= OnRaceStarted;
            m_raceStateTracker.EventOnCompleted -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            if (m_raceResultTime.RecordWasSet)
            {
                m_playerRecordObject.SetActive(true);
                m_playerRecordTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.PlayerRecordTime);
            }
        }

        private void OnRaceCompleted()
        {
            m_playerRecordObject.SetActive(false);
        }
    }
}
