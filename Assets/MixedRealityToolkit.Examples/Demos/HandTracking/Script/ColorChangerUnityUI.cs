using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    class ColorChangerUnityUI : MonoBehaviour
    {
        [SerializeField]
        private Graphic graphic;

        private void Start()
        {
            if (graphic == null)
            {
                graphic = GetComponent<Graphic>();
            }
        }

        public void ChangeColor()
        {
            graphic.color = UnityEngine.Random.ColorHSV();
        }
    }
}
