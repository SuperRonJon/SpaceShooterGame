using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour {

    private void Awake()
    {
        MeshRenderer meshRender = GetComponent<MeshRenderer>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 meshSize = meshRender.bounds.size;

        Vector2 scale = transform.localScale;
        scale *= cameraSize.x / meshSize.x;

        //transform.position = Vector2.zero;
        transform.localScale = scale;

    }
}
