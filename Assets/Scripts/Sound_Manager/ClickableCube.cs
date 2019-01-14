using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Toolz.Utils
{
    [AddComponentMenu("Toolz/Utils/ClickableCube")]
    public class ClickableCube : MonoBehaviour
    {
       
        public UnityEvent EventTriggered;

        private void Start() {
        }

        private void OnMouseUp() {
            EventTriggered.Invoke();
        }
    }
}
