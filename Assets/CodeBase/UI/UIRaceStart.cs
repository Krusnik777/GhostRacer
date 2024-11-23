using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIRaceStart : MonoBehaviour, IDependency<RaceStarter>
    {
        [SerializeField] private Button m_startButton;
        [SerializeField] private GameObject m_panel;

        private RaceStarter m_raceStarter;
        public void Construct(RaceStarter raceStarter) => m_raceStarter = raceStarter;

        private void Start()
        {
            m_startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            m_startButton.onClick.RemoveListener(OnStartButtonClicked);

            m_raceStarter.Begin();

            m_panel.SetActive(false);
        }
    }
}
