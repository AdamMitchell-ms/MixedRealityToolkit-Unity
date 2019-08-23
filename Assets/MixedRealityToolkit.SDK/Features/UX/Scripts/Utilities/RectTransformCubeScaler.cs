using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class RectTransformCubeScaler : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float minOrMaxLerpFactor = 0.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float xOrYLerpFactor = 0.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float minMaxOrXYLerpFactor = 0.0f;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        var size = rectTransform.rect.size;

        var min = Mathf.Min(size.x, size.y);
        var max = Mathf.Max(size.x, size.y);
        var minOrMax = Mathf.Lerp(min, max, minOrMaxLerpFactor);
        var xOrY = Mathf.Lerp(size.x, size.y, xOrYLerpFactor);

        var z = Mathf.Lerp(minOrMax, xOrY, minMaxOrXYLerpFactor);

        this.transform.localScale = new Vector3(size.x, size.y, z);
    }
}
