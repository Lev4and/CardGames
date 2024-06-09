using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Trees;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace CardGames.OpenGLGameEngine.Models.Physics
{
    public struct Ray
    {
        public float MaximumT;
        public Vector3 Origin;
        public Vector3 Direction;
    }

    public struct RayHit
    {
        public float T;
        public bool Hit;
        public Vector3 Normal;
        public CollidableReference Collidable;
    }

    public unsafe struct StaticHitHandler : IRayHitHandler
    {
        public RayHit RayHit;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllowTest(CollidableReference collidable)
        {
            if (collidable.Mobility == CollidableMobility.Static)
            {
                return true;
            }

            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllowTest(CollidableReference collidable, int childIndex)
        {
            if (collidable.Mobility == CollidableMobility.Static)
            {
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnRayHit(in RayData ray, ref float maximumT, float t, in Vector3 normal, 
            CollidableReference collidable, int childIndex)
        {
            if (t < RayHit.T)
            {
                RayHit = new RayHit()
                {
                    T = t,
                    Hit = true,
                    Normal = normal,
                    Collidable = collidable,
                };
            }
        }
    }
}
