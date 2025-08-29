using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevAuraController : MonoBehaviour
{
    [SerializeField]
    string targetLayerName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(targetLayerName))
            return;
        Manager.Game.PrevGame();
    }
}
