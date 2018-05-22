using UnityEngine;
using Object = UnityEngine.Object;

namespace Graphics
{
    public class BloodPool
    {
        #region Fields
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
                    name = Helps.BloodStorageName
                };
            }

            bloodTextures = new Blood[Helps.BloodLimit];
            for (int index = 0; index < Helps.BloodLimit; index++)
            {
                var bloodObject = Object.Instantiate(bloodPrefab, bloodStorage.transform);
                var bloodTexture = bloodObject.GetComponent<Blood>();

                bloodTextures[index] = bloodTexture;
            }
        }


        internal void Create(Vector2 position, Quaternion rotation)
        {
            for (int index = 0; index < Helps.BloodLimit; index++)
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