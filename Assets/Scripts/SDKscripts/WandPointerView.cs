using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace TiltFiveDemos
{




    /// <summary>
    /// Select with the direction directly from the glasses to the wand.
    /// </summary>
    public class WandPointerView : WandPointer
    {

     


      
        public GameObject terminalObject;

        /*
         * Stops 
         */
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private new void Update()
        {

         /*there were issues with accessing the terminal
*         when the tilt five wand used the raycast to interact with UI.
*         To resolve the issue, the raycast is turned off when the terminal is active
*            and all access is handed over to the keyboard user.*/
       
            if(terminalObject != null)
            {
             if(_active &&terminalObject.activeInHierarchy == false)
                        {
                            StartRaycast();
                        }
            }
           
        }

        /// <summary>
        /// Perform a raycast using the direction from the glasses to the wand.
        /// </summary>
        protected override void StartRaycast()
        {
            Vector3 direction = (_pointerOrigin.position - _glassesCamera.transform.position).normalized;

            DoRaycast(direction);
           // Debug.Log("Raycasting in wandpointerview.cs");

            base.StartRaycast();
        }
    }
}
