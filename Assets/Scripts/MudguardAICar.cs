using System;
using UnityEngine;


    public class MudguardAICar : MonoBehaviour
    {
        public CarController carController; // car controller to get the steering angle

        private Quaternion m_OriginalRotation;


        private void Start()
        {
            m_OriginalRotation = transform.localRotation;
        }

        // Rotate Mudgaurd with wheel steer
        private void Update()
        {
            transform.localRotation = m_OriginalRotation*Quaternion.Euler(0, carController.CurrentSteerAngle, 0);
        }
    
}
