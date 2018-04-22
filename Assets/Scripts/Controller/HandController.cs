using UI;
using UnityEngine;

namespace Controller
{
    public class HandController
    {
        private readonly Transform axis; //плечо
        private readonly Transform gunpoint; //

        public Transform GunPoint { get { return gunpoint; } }

        private Quaternion gunPointRot;

        public HandController(HeroInfo heroInfo)
        {
            axis = heroInfo.HandAxis;
            gunpoint = heroInfo.Gunpoint;

            var asas = gunpoint.position - gunpoint.parent.position;
            gunPointRot = gunpoint.rotation;
        }

        public float Delta = 1f;//target offset for parent (by Y)

        private void AdjustParent(Transform tr, Vector3 target, int parentDepth)
        {
            if (parentDepth == 0 || tr == null) return;

            AdjustParent(tr.parent, target + new Vector3(0, -Delta), parentDepth - 1);

            tr.up = -(target - tr.position);
        }

        public void LookTo(Vector3 aim)
        {
            //AdjustParent(gunpoint.transform, aim, 4);
            var tr = new Transform[3];
            var target = new Vector3[3];
            
            
            tr[0] = gunpoint.parent.transform;
            if (tr[0] == null) return;
            target[0] = aim;
            
            tr[1] = tr[0].parent;
            if (tr[1] == null) return;
            target[1] = target[0] + new Vector3(0, -Delta);
            
            tr[2] = tr[1].parent;
            if (tr[2] == null) return;
            target[2] = target[1] + new Vector3(0, -Delta);
            
            axis.up = -(target[2] - tr[2].position);
            tr[1].up = -(target[1] - tr[1].position);
            gunpoint.up = -(target[0] - tr[0].position);
            
        }
    }
}