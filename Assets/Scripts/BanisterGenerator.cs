#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEditor;

public class BanisterGenerator : MonoBehaviour
{
    public GameObject BalusterPrefab;
    public int intermediaryBalustersAmount;

    public List<GameObject> GenerateBanisters()
    {
        Vertex[] vertices = GetComponent<ProBuilderMesh>().GetVertices();
        List<Vector3> vertPosList = new List<Vector3>();

        float highestY = 0;

        // Remove vertices with lower y positions to get those of the top face.
        // Find highest Y value
        foreach (var vert in vertices)
        {
            if (vert.position.y > highestY)
            {
                highestY = vert.position.y;
            }
        }

        // Remove those below the highest y value
        foreach (var vert in vertices)
        {
            if (vert.position.y == highestY && !vertPosList.Contains(vert.position)) // Don't add duplicates.
            {
                vertPosList.Add(vert.position);
            }
        }

        // Now work out which are on the corners
        float highestX = -1000;
        float lowestX = 1000;
        float highestZ = -1000;
        float lowestZ = 1000;

        foreach (var vertPos in vertPosList)
        {
            if (vertPos.x > highestX)
            {
                highestX = vertPos.x;
            }

            if (vertPos.x < lowestX)
            {
                lowestX = vertPos.x;
            }

            if (vertPos.z > highestZ)
            {
                highestZ = vertPos.z;
            }

            if (vertPos.z < lowestZ)
            {
                lowestZ = vertPos.z;
            }
        }

        Vector3 topRight = new Vector3();
        Vector3 bottomRight = new Vector3();
        Vector3 topLeft = new Vector3();
        Vector3 bottomLeft = new Vector3();

        foreach (var vertPos in vertPosList)
        {
            // Get only the corners
            if (vertPos.x == highestX & vertPos.z == highestZ) // top right
            {
                topRight = vertPos;
            }
                
            if (vertPos.x == lowestX & vertPos.z == highestZ) // bottom right
            {
                bottomRight = vertPos;
            }
                    
            if (vertPos.x == highestX & vertPos.z == lowestZ) // top left
            {
                topLeft = vertPos;
            }
                        
            if (vertPos.x == lowestX & vertPos.z == lowestZ) // bottom left
            {
                bottomLeft = vertPos;
            }
        }

        List<GameObject> finalBanisterGOs = new List<GameObject>();

        // Make more balusters between top right and bottom right
        void createBalusters(Vector3 rightCorner, Vector3 leftCorner)
        {
            for (int i = 0; i <= intermediaryBalustersAmount; i++)
            {
                float lerpValue = i / (float)intermediaryBalustersAmount;
                Vector3 spawnPoint = Vector3.Lerp(rightCorner, leftCorner, lerpValue);
                GameObject baluster = (GameObject)PrefabUtility.InstantiatePrefab(BalusterPrefab, transform);
                baluster.transform.localPosition = spawnPoint + new Vector3(0f, 1f, 0f);

                finalBanisterGOs.Add(baluster);
            }
        }

        createBalusters(topRight, topLeft);
        createBalusters(bottomRight, bottomLeft);

        // Now create handrail
        void createHandrail(Vector3 left, Vector3 right)
        {
            GameObject handrail = GameObject.CreatePrimitive(PrimitiveType.Cube);
            handrail.transform.SetParent(transform, false);

            float cornerDistance = Vector3.Distance(left, right);
            handrail.transform.localScale = new Vector3(0.2f, 0.1f, cornerDistance);
            handrail.transform.localPosition = new Vector3(left.x, 2.175f, -cornerDistance / 2);
            handrail.name = "Handrail";

            finalBanisterGOs.Add(handrail);
        }

        createHandrail(topLeft, topRight);
        createHandrail(bottomLeft, bottomRight);

        return finalBanisterGOs;
    }
}

#endif