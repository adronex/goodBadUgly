using UnityEngine;

namespace Graphics
{
    public class BloodHolePool
    {
        #region Fields
        private int MAX_BLOOD = 30;

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
            }

            bloodHoles = new GameObject[MAX_BLOOD];
            for (int i = 0; i < MAX_BLOOD; i++)
            {
                bloodHoles[i] = Object.Instantiate(bloodHolePrefab, bloodHolesStorage.transform);
                bloodHoles[i].SetActive(false);
            }
        }


        internal void Create(Transform bullet)
        {
            var offset = Random.Range(0, 0.7f);
            var position = bullet.position + bullet.right * offset * 50 * Time.deltaTime;

            var ass = Physics2D.CircleCastAll(position, 0.1f, Vector2.zero);

            for (int i = 0; i < ass.Length; i++)
            {
                var bloodHole = GetNext();

                bloodHole.transform.position = position;
                bloodHole.transform.parent = ass[i].transform;

                if (!bloodHoles[i].activeSelf)
                {
                    bloodHoles[i].SetActive(true);
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