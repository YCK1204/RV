using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    BoxCollider2D _boxCollider;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        Invoke("OnCollider", .1f);
    }
    void OnCollider()
    {
        _boxCollider.enabled = true;
    }
}
