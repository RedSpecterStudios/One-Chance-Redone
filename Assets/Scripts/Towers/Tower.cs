using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    internal Material GhostMaterialObject;

    public Material GhostMaterial => GhostMaterialObject;
    public Transform GemBase;
}
