using UnityEngine;

namespace Racing
{
    public class RaceController : MonoBehaviour
    {
        public static RaceController Instance;

        public static RaceData RaceData => Instance.raceData;

        private RaceData raceData;

        public void ResetData()
        {
            raceData = new RaceData();
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            ResetData();
        }
    }
}
