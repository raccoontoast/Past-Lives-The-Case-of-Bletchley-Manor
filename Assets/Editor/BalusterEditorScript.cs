using UnityEngine;
using UnityEngine.ProBuilder;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

//[CustomEditor(typeof(BanisterGenerator)), CanEditMultipleObjects]
public class BalusterEditorScript : EditorWindow
{
    //private float _space = 20f;
    private int _intermediaryBanistersAmount;
    private GameObject _balusterPrefab = null;
    private List<GameObject> _balusterList;
    private Material _handrailMaterial;

    [MenuItem("Custom Editor/Banisters")]
    public static void CustomEditorWindow()
    {
        GetWindow<BalusterEditorScript>("Banisters Window");
    }

    private void OnGUI()
    {
        _intermediaryBanistersAmount = EditorGUI.IntField(new Rect(3, 3, position.width, 20), "Intermediary Banisters", _intermediaryBanistersAmount);
        _balusterPrefab = (GameObject)EditorGUI.ObjectField(new Rect(3, 23, position.width, 20), "Baluster Prefab", _balusterPrefab, typeof(GameObject), true);
        _handrailMaterial = (Material)EditorGUI.ObjectField(new Rect(3, 43, position.width, 20), "Handrail Texture", _handrailMaterial, typeof(Material), true);


        GUILayout.Space(75f);
        if (GUILayout.Button("Generate Banisters"))
        {
            foreach (var GO in Selection.gameObjects)
            {
                _balusterList = GenerateBanisters(GO, _intermediaryBanistersAmount, _balusterPrefab);
            }
        }

        GUILayout.Space(25f);
        if (GUILayout.Button("Delete Last Banisters"))
        {
            foreach (var GO in _balusterList)
            {
                DestroyImmediate(GO);
            }
        }

        GUILayout.Space(25f);
        if (GUILayout.Button("Delete All Banisters on Selected"))
        {
            int childCount;

            foreach (var GO in Selection.gameObjects)
            {
                childCount = GO.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    Transform toBeDestroyed = GO.transform.Find("Baluster");

                    if (toBeDestroyed != null)
                    {
                        DestroyImmediate(toBeDestroyed.gameObject);
                    }
                }

                childCount = GO.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    Transform toBeDestroyed = GO.transform.Find("Handrail");

                    if (toBeDestroyed != null)
                    {
                        DestroyImmediate(toBeDestroyed.gameObject);
                    }
                }
            }
        }

        GUILayout.Space(25f);
        if (GUILayout.Button("Apply Handrail Material"))
        {
            foreach (var GO in Selection.gameObjects)
            {
                int childCount = GO.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    Transform toHaveMaterialApplied = GO.transform.Find("Handrail");

                    if (toHaveMaterialApplied != null)
                    {
                        toHaveMaterialApplied.GetComponent<Renderer>().material = _handrailMaterial;
                        toHaveMaterialApplied.name += "withMaterial";
                    }
                }
            }
        }
    }

    public List<GameObject> GenerateBanisters(GameObject gameObject, int intermediaryBalustersAmount, GameObject balusterPrefab)
    {
        Vertex[] vertices = gameObject.GetComponent<ProBuilderMesh>().GetVertices();
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
                GameObject baluster = (GameObject)PrefabUtility.InstantiatePrefab(balusterPrefab, gameObject.transform);
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
            handrail.transform.SetParent(gameObject.transform, false);

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

#if UNITY_EDITOR

    //List<GameObject> balusterList = new List<GameObject>();

    //override public void OnInspectorGUI()
    //{
    //    BanisterGenerator banisterGenerator = (BanisterGenerator)target;
    //    if (GUILayout.Button("Generate Balusters"))
    //    {
    //        balusterList = banisterGenerator.GenerateBanisters();
    //    }

    //    if (GUILayout.Button("Delete Balusters"))
    //    {
    //        foreach (var baluster in balusterList)
    //        {
    //            DestroyImmediate(baluster);
    //        }
    //    }

    //    if (GUI.changed)
    //    {
    //        EditorUtility.SetDirty(target);
    //    }
    //    DrawDefaultInspector();
    //}


#endif
}
