using UnityEngine;

namespace Racing
{
    public class RaceData
    {
        public float RecordTime;
        public Vector3[] Positions;
        public float[] Speeds;

        public RaceData()
        {
            RecordTime = 0;
        }
    }
}
