using Ashsvp;
using UnityEngine;

namespace Racing
{
    public class RaceStarter : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<SimcadeVehicleController>
    {
        private RaceStateTracker m_raceStateTracker;
        public void Construct(RaceStateTracker raceStateTracker) => m_raceStateTracker = raceStateTracker;
        private SimcadeVehicleController m_vehicleController;
        public void Construct(SimcadeVehicleController vehicleController) => m_vehicleController = vehicleController;

        public void Begin()
        {
            m_raceStateTracker.LaunchPreparationStart();
            enabled = false;
        }

        private void Start()
        {
            m_vehicleController.CanDrive = false;
        }
    }
}
