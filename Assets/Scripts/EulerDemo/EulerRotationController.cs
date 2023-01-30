using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Graduation.Euler
{
    public class EulerRotationController : MonoBehaviour
    {
        [SerializeField] Transform center;
        [SerializeField] Button xButton;
        [SerializeField] Button yButton;
        [SerializeField] Button zButton;
        [SerializeField] Button resetButton;

        RotationElement element;
        float moveTime = 0.5f;

        private void Awake()
        {
            element = new RotationElement(center, moveTime);
        }

        // Start is called before the first frame update
        void Start()
        {
            xButton.onClick.AddListener(OnClickedXButton);
            yButton.onClick.AddListener(OnClickedYButton);
            zButton.onClick.AddListener(OnClickedZButton);
            resetButton.onClick.AddListener(OnClickedResetButton);
        }

        // Update is called once per frame
        void Update()
        {
            element.OnUpdate();
        }

        private void OnClickedXButton()
        {
            element.StartRotation(Vector3.right, 90);
        }

        private void OnClickedYButton()
        {
            element.StartRotation(Vector3.up, 90);
        }

        private void OnClickedZButton()
        {
            element.StartRotation(Vector3.forward, 90);
        }

        private void OnClickedResetButton()
        {
            center.rotation = Quaternion.identity;
        }
    }
}
