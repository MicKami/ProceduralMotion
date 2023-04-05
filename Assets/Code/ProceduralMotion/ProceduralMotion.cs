namespace MicKami.ProceduralMotion
{
    //Credits:
    //Inspired by a video tutorial (https://youtu.be/KPoeNZZ6H4s) by @t3ssel8r.

    using UnityEngine;
    using static UnityEngine.Mathf;

    [System.Serializable]
    public class ProceduralMotion
    {
        [field: SerializeField, Tooltip("Natural frequency")]
        public float F { get; set; } = 2f;
        [field: SerializeField, Tooltip("Damping ratio")]
        public float Z { get; set; } = 0.5f;
        [field: SerializeField, Tooltip("Initial response")]
        public float R { get; set; } = 1f;

        private float k1 => Z / (PI * F);
        private float k2 => 1 / ((2 * PI * F) * (2 * PI * F));
        private float k3 => R * Z / (2 * PI * F);

        #region UpdateFloat
        float velocity1D;
        float? previousTargetPosition1D = null;
        public float UpdateFloat(float current, float target, float dt)
        {
            previousTargetPosition1D ??= current;
            float targetVelocity = (target - (float)previousTargetPosition1D) / dt;
            previousTargetPosition1D = target;
            float k2_stable = Max(k2, dt * dt / 2 + dt * k1 / 2, dt * k1);
            float result = current + velocity1D * dt;
            velocity1D = velocity1D + dt * (target + k3 * targetVelocity - result - k1 * velocity1D) / k2_stable;
            return result;
        }
        #endregion

        #region UpdateVector2
        Vector2 velocity2D;
        Vector2? previousTargetPosition2D = null;
        public Vector2 UpdateVector2(Vector2 current, Vector2 target, float dt)
        {
            previousTargetPosition2D ??= current;
            Vector2 targetVelocity = (target - (Vector2)previousTargetPosition2D) / dt;
            previousTargetPosition2D = target;
            float k2_stable = Max(k2, dt * dt / 2 + dt * k1 / 2, dt * k1);
            Vector2 result = current + velocity2D * dt;
            velocity2D = velocity2D + dt * (target + k3 * targetVelocity - result - k1 * velocity2D) / k2_stable;
            return result;
        }
        #endregion

        #region UpdateVector3
        Vector3 velocity3D;
        Vector3? previousTargetPosition3D = null;
        public Vector3 UpdateVector3(Vector3 current, Vector3 target, float dt)
        {
            previousTargetPosition3D ??= current;
            Vector3 targetVelocity = (target - (Vector3)previousTargetPosition3D) / dt;
            previousTargetPosition3D = target;
            float k2_stable = Max(k2, dt * dt / 2 + dt * k1 / 2, dt * k1);
            Vector3 result = current + velocity3D * dt;
            velocity3D = velocity3D + dt * (target + k3 * targetVelocity - result - k1 * velocity3D) / k2_stable;
            return result;
        }
        #endregion

        #region UpdateQuaternion
        Vector4 velocity4D;
        Vector4? previousTargetPosition4D = null;
        public Quaternion UpdateQuaternion(Quaternion current, Quaternion target, float dt)
        {
            Vector4 currentRotation = new Vector4(current.x, current.y, current.z, current.w);
            Vector4 targetRotation = new Vector4(target.x, target.y, target.z, target.w);
            previousTargetPosition4D ??= currentRotation;
            Vector4 targetVelocity = (targetRotation - (Vector4)previousTargetPosition4D) / dt;
            previousTargetPosition4D = targetRotation;
            float k2_stable = Max(k2, dt * dt / 2 + dt * k1 / 2, dt * k1);
            Vector4 result = currentRotation + velocity4D * dt;
            velocity4D = velocity4D + dt * (targetRotation + k3 * targetVelocity - result - k1 * velocity4D) / k2_stable;
            return new Quaternion(result.x, result.y, result.z, result.w);
        }
        #endregion

        public ProceduralMotion Copy()
        {
            ProceduralMotion result = new ProceduralMotion
            {
                F = F,
                Z = Z,
                R = R
            };
            return result;
        }
    }
}