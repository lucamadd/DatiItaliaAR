using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField]
        GameObject m_BackButton;

        Canvas RegionCanvas;

        public GameObject backButton
        {
            get => m_BackButton;
            set => m_BackButton = value;
        }


        void Start()
        {
            if (Application.CanStreamedLevelBeLoaded("Menu"))
            {
                m_BackButton.SetActive(true);
            }
        }

        public void Update()
        {
            var CanvasParent = GameObject.FindGameObjectWithTag("canvas");
            RegionCanvas = CanvasParent.GetComponentInChildren<Canvas>(true);
            if (CanvasParent.transform.localScale.y < 0.01f)
            {
                RegionCanvas.gameObject.SetActive(false);
            } else {
                RegionCanvas.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButtonPressed();
            }
        }

        public void BackButtonPressed()
        {
            var CanvasParent = GameObject.FindGameObjectWithTag("canvas");
            RegionCanvas = CanvasParent.GetComponentInChildren<Canvas>(true);
            
            if (RegionCanvas.gameObject.activeSelf)
            {
                //uso questo come valore speciale perché nell'altro script quando ho questo valore allora capisco che devo chiudere
                CanvasParent.transform.localScale = new Vector3(1f, 1.001f, 1f);
            }
            
            else if (Application.CanStreamedLevelBeLoaded("Menu"))
            {
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                LoaderUtility.Deinitialize();
            }

        }

    }

}
