using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    [Range(0, 1)]
    public float alpha = 1;

    private Color col;
    private Renderer rend;

	void Start () {
        rend = GetComponent<Renderer>();
        col = Color.white;
	}
	
	void Update () {
        col.a = alpha;
        rend.material.color = col;
	}
}
