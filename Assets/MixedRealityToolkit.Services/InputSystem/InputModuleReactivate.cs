using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MixedRealityToolkit.Services.InputSystem
{
    public class InputModuleReactivate : MonoBehaviour
    {
        private void Start()
        {
            MixedRealityInputModule.Reactivate();
        }
    }
}
