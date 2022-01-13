using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class RegionButton : MonoBehaviour
    {
        [SerializeField]
        GameObject m_BackButton;

        [SerializeField]
        Canvas RegionCanvas;
        public GameObject backButton
        {
            get => m_BackButton;
            set => m_BackButton = value;
        }

        void Start()
        {
            m_BackButton.SetActive(true);
            
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButtonPressed();
            }
        }

        public void BackButtonPressed()
        {
            RegionCanvas.gameObject.SetActive(false);
        }

        
    }
}
