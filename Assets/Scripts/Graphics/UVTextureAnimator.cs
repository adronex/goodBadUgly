using System.Collections;
using UnityEngine;

namespace Graphics
{
    public class UVTextureAnimator : MonoBehaviour
    {
        #region Fields
        [SerializeField] private int rows = 8;
        [SerializeField] private int columns = 4;
        [SerializeField] private float fps = 60;

        private int index;
        private int count, allCount;
        private float deltaFps;
        private Renderer currentRenderer;
        private Material instanceMaterial;
        #endregion

        #region Properties
        internal Blood Blood { get; set; }
        #endregion

        #region Unity lifecycle
        private void Awake()
        {
            deltaFps = 1f / fps;
            count = rows * columns;
            currentRenderer = GetComponent<Renderer>();
            currentRenderer.sortingOrder = 100;
        }


        void OnDestroy()
        {
            if (instanceMaterial != null)
            {
                Destroy(instanceMaterial);
                instanceMaterial = null;
            }
        }
        #endregion

        #region Public methods
        internal void Play()
        {
            ResetToDefault();
            gameObject.SetActive(true);

            StartCoroutine(UpdateCorutine());
        }
        #endregion

        #region Private methods
        private void ResetToDefault()
        {
            allCount = 0;
            index = columns - 1;

            instanceMaterial = currentRenderer.material;

            var size = new Vector2(1f / columns, 1f / rows);
            instanceMaterial.SetTextureScale("_MainTex", size);

            var offset = Vector3.zero;
            instanceMaterial.SetTextureOffset("_MainTex", offset);

        }


        private IEnumerator UpdateCorutine()
        {
            while (allCount != count)
            {
                UpdateCorutineFrame();
                if (allCount == count)
                {
                    break;
                }
                yield return new WaitForSeconds(deltaFps);
            }

            gameObject.SetActive(false);
            Blood.IncrementDisable();
        }


        private void UpdateCorutineFrame()
        {
            allCount++;
            index++;
            if (index >= count)
            {
                index = 0;
            }

            var offsetX = (float)index / columns - (index / columns);
            var offsetY = 1 - (index / columns) / (float)rows;

            var offset = new Vector2(offsetX, offsetY);

            if (currentRenderer != null)
            {
                instanceMaterial.SetTextureOffset("_MainTex", offset);
            }
        }
        #endregion
    }
}