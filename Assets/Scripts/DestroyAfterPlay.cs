using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour {

	void DestroySelf()
    {
        Destroy(gameObject);
    }
}
