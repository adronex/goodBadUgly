using UnityEngine;

namespace Graphics
{
    public class Blood : MonoBehaviour
    {
        #region Fields
        private UVTextureAnimator[] bloodTextures;

        private int disableCount;
        #endregion

        #region Properties
        internal bool IsBusy
        {
            get
            {
                return bloodTextures.Length <= disableCount;
            }
        }
        #endregion

        #region Unity lifecycle
        private void Awake()
        {
            var child = transform.GetChild(0);

            var bloodPartsCount = child.childCount;
            bloodTextures = new UVTextureAnimator[bloodPartsCount];

            for (int index = 0; index < bloodPartsCount; index++)
            {
                var bloodPart = child.GetChild(index);
                var bloodTexture = bloodPart.GetComponent<UVTextureAnimator>();
                bloodTexture.Blood = this;
                bloodTextures[index] = bloodTexture;
            }

            gameObject.SetActive(false);
        }
        #endregion

        #region Public methods
        internal void IncrementDisable()
        {
            disableCount++;
            if (!IsBusy)
            {
                gameObject.SetActive(false);
            }
        }

        
        internal void Play()
        {
            disableCount = 0;

            gameObject.SetActive(true);
            foreach (var bloodTexture in bloodTextures)
            {
                bloodTexture.Play();
            }
        }
    }
    #endregion
}