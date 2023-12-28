using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWatchMesh : MonoBehaviour
{

    Mesh mesh;


    Vector3[] vertexPos;
    [SerializeField] float distance;
    [SerializeField] float aralik,yukseklik;


    // ust vertex
    //0->19,22
    //3->10,23
    //alt vertex
    //15->16,21
    //20-> 9,12

    private void Awake()
    {

        mesh = GetComponent<MeshFilter>().mesh;
        vertexPos = mesh.vertices;
        Percinle(0, 19, 22, aralik, yukseklik);
        Percinle(3, 10, 23, -aralik, yukseklik);

        Percinle(15, 16, 21, aralik, -yukseklik);
        Percinle(20, 9, 12, -aralik, -yukseklik);


    }


    //private void FixedUpdate()
    //{
    //    Percinle(0, 19, 22,aralik,yukseklik);
    //    Percinle(3, 10, 23,-aralik,yukseklik);

    //    Percinle(15, 16, 21,aralik, -yukseklik);
    //    Percinle(20, 9, 12, -aralik, -yukseklik);


    //}

    private void Percinle(int index0,int index1,int index2,float aralik,float yukseklik)
    {
        vertexPos[index0].z = distance;
        vertexPos[index0].x += aralik / 2;
        vertexPos[index0].y += yukseklik / 2;

        vertexPos[index1] = vertexPos[index0];
        vertexPos[index2] = vertexPos[index0];

        this.mesh.vertices = vertexPos;

    }

}


