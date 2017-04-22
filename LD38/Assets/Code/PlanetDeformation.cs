using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDeformation : MonoBehaviour
{
    #region Public Data

    #endregion

    #region Private Data
    private Mesh _planetMesh;
    private MeshCollider _planetMeshCollider;
    #endregion

    #region Public API
    void Start ()
    {
        _planetMesh = GetComponent<MeshFilter>().mesh;
        _planetMeshCollider = GetComponent<MeshCollider>();
	}
	
    public void ExplodeAt(Vector3 center, float radius) 
    {
        // access vertex list
        Vector3[] VertexList = _planetMesh.vertices;
        Vector3[] NormalList = _planetMesh.normals;

        for(int i = 0; i < VertexList.Length; i++)
        {
            Vector3 refVert = VertexList[i];
            Vector3 checkVert = VertexList[i];

            checkVert *= transform.localScale.x;
            checkVert = transform.rotation * checkVert;
            checkVert += transform.position;

            float dist = Vector3.Distance(center, checkVert);

            if (dist <= radius)
            {
                // inside sphere

                float amount = (dist / radius);

                refVert -= NormalList[i] * 1 / transform.localScale.x * amount;

                VertexList[i] = refVert;
            }
        }

        _planetMesh.vertices = VertexList;

        _planetMesh.RecalculateBounds();
        _planetMesh.RecalculateNormals();

        _planetMeshCollider.sharedMesh = _planetMesh;
    }
    #endregion
}
