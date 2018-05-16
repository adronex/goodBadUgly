using UnityEngine;

namespace Graphics
{
    public class BloodHolePool
    {
        #region Fields
        private int MAX_BLOOD = 50;

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
                    name = "BloodHolesStorage"
                };
                bloodHolesStorage.SetActive(false);
            }

            bloodHoles = new GameObject[MAX_BLOOD];
            for (int i = 0; i < MAX_BLOOD; i++)
            {
                bloodHoles[i] = Object.Instantiate(bloodHolePrefab, bloodHolesStorage.transform);
               // bloodHoles[i].SetActive(false);
            }
        }


        internal void Create(Vector3 position)
        {
            var az = Physics2D.CircleCastAll(position, 0.3f, Vector2.zero);//

            for (int i = 0; i < az.Length; i++)
            {
                var bloodHole = GetNext();

                bloodHole.transform.position = position;
                bloodHole.transform.parent = az[i].transform;

                var bodyPartMask = az[i].transform.GetComponent<SpriteMask>();
                var bloodRenderer = bloodHole.GetComponent<SpriteRenderer>();

                bloodRenderer.sortingLayerID = bodyPartMask.frontSortingLayerID;
                bloodRenderer.sortingOrder = bodyPartMask.frontSortingOrder - 1;


                if (!bloodHoles[i].activeSelf)
                {
                  //  bloodHoles[i].SetActive(true);
                }
            }
        }
        #endregion

        #region Private methods
        private GameObject GetNext()
        {
            if (currentIndex >= MAX_BLOOD)
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