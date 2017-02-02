using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    internal Material GhostMaterialObject = null;

    public Material GhostMaterial => GhostMaterialObject;
    public Transform GemBase = null;
}
