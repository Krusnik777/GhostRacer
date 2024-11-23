using Ashsvp;
using UnityEngine;

namespace Racing
{
    public class DependenciesContainer : Dependency
    {
        [SerializeField] private Pauser m_pauser;
        [SerializeField] private RaceStateTracker m_raceStateTracker;
        [SerializeField] private RaceTimeTracker m_raceTimeTracker;
        [SerializeField] private RaceResultTime m_raceResultTime;
        [SerializeField] private TrackpointCircuit m_trackpointCircuit;
        [SerializeField] private SimcadeVehicleController m_vehicleController;
        [SerializeField] private RaceStarter m_raceStarter;

        private void Awake()
        {
            FindAllObjectsToBind();
        }


        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Pauser>(m_pauser, monoBehaviourInScene);
            Bind<RaceStateTracker>(m_raceStateTracker, monoBehaviourInScene);
            Bind<RaceTimeTracker>(m_raceTimeTracker, monoBehaviourInScene);
            Bind<RaceResultTime>(m_raceResultTime, monoBehaviourInScene);
            Bind<TrackpointCircuit>(m_trackpointCircuit, monoBehaviourInScene);
            Bind<SimcadeVehicleController>(m_vehicleController, monoBehaviourInScene);
            Bind<RaceStarter>(m_raceStarter, monoBehaviourInScene);
        }
    }
}
