using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Racing
{
    public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
    {
        [SerializeField] private GameObject m_resultsPanel;
        [SerializeField] private TextMeshProUGUI m_playerCurrentTimeText;
        [SerializeField] private GameObject m_newRecordObject;
        [SerializeField] private Button m_restartButton;
        [SerializeField] private Button m_resetButton;

        private RaceResultTime m_raceResultTime;
        public void Construct(RaceResultTime raceResultTime) => m_raceResultTime = raceResultTime;

        private void Start()
        {
            m_raceResultTime.EventOnUpdatedResults += OnUpdatedResults;

            m_resultsPanel.SetActive(false);

            m_restartButton.onClick.AddListener(OnRestart);
            m_resetButton.onClick.AddListener(OnReset);
        }


        private void OnDestroy()
        {
            m_raceResultTime.EventOnUpdatedResults -= OnUpdatedResults;

            m_restartButton.onClick.RemoveListener(OnRestart);
            m_resetButton.onClick.RemoveListener(OnReset);
        }

        private void OnUpdatedResults()
        {
            m_resultsPanel.SetActive(true);

            m_playerCurrentTimeText.text = StringTime.SecondToTimeString(m_raceResultTime.CurrentTime);

            if (!m_raceResultTime.RecordWasSet)
            {
                m_newRecordObject.SetActive(false);
            }
            else
            {
                if (m_raceResultTime.CurrentTime <= m_raceResultTime.PlayerRecordTime)
                {
                    SetRecordObjectsByResult(true);
                }
                else
                {
                    SetRecordObjectsByResult(false);
                }
            }
        }

        private void SetRecordObjectsByResult(bool newRecordWasSet)
        {
            m_newRecordObject.SetActive(newRecordWasSet);
        }

        private void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnReset()
        {
            RaceController.Instance.ResetData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
