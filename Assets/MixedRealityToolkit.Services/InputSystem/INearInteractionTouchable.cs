using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Input
{
    public interface INearInteractionTouchable
    {
        Vector3 LocalCenter { get; }
        Vector3 LocalForward { get; }
        Vector3 LocalUp { get; }
        Vector3 LocalRight { get; }

        Vector3 Forward { get; }
        Vector2 Bounds { get; }
        Transform transform { get; }
    }
}
