using UnityEngine;
using Object = UnityEngine.Object;

namespace Graphics
{
    public class BloodPool
    {
        #region Fields
        private const int MAX_BLOOD = 2;

        private Blood[] bloodTextures;

        private static GameObject bloodStorage;
        #endregion

        #region Public Fields
        internal BloodPool(GameObject bloodPrefab)
        {
            if (bloodStorage == null)
            {
                bloodStorage = new GameObject
                {
                    name = "BloodStorage"
                };
            }

            bloodTextures = new Blood[MAX_BLOOD];
            for (int index = 0; index < MAX_BLOOD; index++)
            {
                var bloodObject = Object.Instantiate(bloodPrefab, bloodStorage.transform);
                var bloodTexture = bloodObject.GetComponent<Blood>();

                bloodTextures[index] = bloodTexture;
            }
        }


        internal void Create(Vector2 position, Quaternion rotation)
        {
            for (int index = 0; index < MAX_BLOOD; index++)
            {
                var bloodTexture = bloodTextures[index];
                if (!bloodTexture.IsBusy)
                {
                    bloodTexture.transform.position = new Vector2(position.x, position.y);
                    bloodTexture.transform.rotation = rotation;

                    bloodTexture.Play();
                    return;
                }
            }
        }
        #endregion
    }
}