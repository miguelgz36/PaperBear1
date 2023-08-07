using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public static class RotationUtils
    {
        public static float CalculateRotationToAimObject(Vector3 source, Vector3 target)
        {
            float angle = Mathf.Atan2(target.y - source.y,
                          target.x - source.x)
              * Mathf.Rad2Deg - 90;
            return angle;
        }
    }
}