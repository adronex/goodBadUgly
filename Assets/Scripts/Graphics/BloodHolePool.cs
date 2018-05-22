using UnityEngine;

namespace Graphics
{
    public class BloodHolePool
    {
        #region Fields
        private GameObject[] bloodHoles;
        private GameObject bloodHolesStorage;
        private int currentIndex;
        #endregion
        #region Public methods
        internal BloodHolePool(GameObject bloodHolePrefab)
        {
            currentIndex = 0;

            if (bloodHolesStorage == null)
            {
                bloodHolesStorage = new GameObject
                {
                    name = Helps.BloodHoleStorageName
                };
                bloodHolesStorage.SetActive(false);
            }

            bloodHoles = new GameObject[Helps.BloodHoleLimit];
            for (int i = 0; i < Helps.BloodHoleLimit; i++)
            {
                bloodHoles[i] = Object.Instantiate(bloodHolePrefab, bloodHolesStorage.transform);
            }
        }


        internal void Create(Vector3 position)
        {
            var bodyParts = Physics2D.CircleCastAll(position, Helps.BloodCreateRadius, Vector2.zero);
            var bodyPartsLength = bodyParts.Length;
            for (int index = 0; index < bodyPartsLength; index++)
            {
                var bloodHole = GetNext();

                bloodHole.transform.position = position;
                bloodHole.transform.parent = bodyParts[index].transform;

                var bodyPartMask = bodyParts[index].transform.GetComponent<SpriteMask>();
                if (bodyPartMask == null)
                {
                    continue;
                }

                var bloodRenderer = bloodHole.GetComponent<SpriteRenderer>();
                if (bloodRenderer)
                {
                    continue;
                }

                bloodRenderer.sortingLayerID = bodyPartMask.frontSortingLayerID;
                bloodRenderer.sortingOrder = bodyPartMask.frontSortingOrder - Helps.BloodHoleSortingLayerOffset;
            }
        }
        #endregion
        #region Private methods
        private GameObject GetNext()
        {
            if (currentIndex >= Helps.BloodHoleLimit)
            {
                currentIndex = 0;
            }

            var bloodHole = bloodHoles[currentIndex];
            currentIndex++;

            return bloodHole;
        }
        #endregion
    }
}