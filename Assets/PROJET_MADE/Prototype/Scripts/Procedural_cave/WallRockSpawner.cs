using System.Collections;
using UnityEngine;

public class WallRockSpawner : MonoBehaviour
{
    [Header("Mesh")]
    public MeshFilter wallMeshFilter;

    [Header("Rocks")]
    public GameObject[] rockPrefabs;

    [Header("Spawn Settings")]
    [Range(0f, 1f)]
    public float spawnChance = 0.04f;

    public float embedDepth = 0.12f;   // 🔥 profondeur dans le mur
    public float surfaceOffset = 0.02f;

    public Vector2 scaleRange = new Vector2(0.7f, 1.4f);

    public float maxUpDot = 0.5f; // filtre sol/plafond

    IEnumerator Start()
    {
        // attendre que le mesh soit généré (important dans le tuto)
        yield return new WaitForSeconds(0.5f);

        if (wallMeshFilter == null || wallMeshFilter.sharedMesh == null)
        {
            Debug.LogError("WallRockSpawner: MeshFilter ou Mesh manquant.");
            yield break;
        }

        if (rockPrefabs == null || rockPrefabs.Length == 0)
        {
            Debug.LogError("WallRockSpawner: Aucun prefab assigné.");
            yield break;
        }

        SpawnRocks();
    }

    void SpawnRocks()
    {
        Mesh mesh = wallMeshFilter.sharedMesh;

        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;

        if (normals == null || normals.Length != vertices.Length)
        {
            mesh.RecalculateNormals();
            normals = mesh.normals;
        }

        for (int i = 0; i < triangles.Length - 2; i += 3)
        {
            if (Random.value > spawnChance)
                continue;

            int i1 = triangles[i];
            int i2 = triangles[i + 1];
            int i3 = triangles[i + 2];

            // sécurité
            if (i1 < 0 || i2 < 0 || i3 < 0)
                continue;

            if (i1 >= vertices.Length ||
                i2 >= vertices.Length ||
                i3 >= vertices.Length)
                continue;

            Vector3 v1 = vertices[i1];
            Vector3 v2 = vertices[i2];
            Vector3 v3 = vertices[i3];

            Vector3 n1 = normals[i1];
            Vector3 n2 = normals[i2];
            Vector3 n3 = normals[i3];

            Vector3 normalLocal = (n1 + n2 + n3).normalized;

            // 🔥 filtre sol / plafond
            if (Mathf.Abs(normalLocal.y) > maxUpDot)
                continue;

            // point random sur triangle
            Vector3 localPoint = RandomPointInTriangle(v1, v2, v3);

            Vector3 worldNormal =
                wallMeshFilter.transform.TransformDirection(normalLocal);

            Vector3 worldPoint =
                wallMeshFilter.transform.TransformPoint(localPoint);

            // 🔥 ENFOUISSEMENT dans le mur (effet fusion)
            worldPoint -= worldNormal * embedDepth;

            // léger offset anti z-fighting
            worldPoint += worldNormal * surfaceOffset;

            GameObject prefab =
                rockPrefabs[Random.Range(0, rockPrefabs.Length)];

            // 🔥 orientation propre collée au mur
            Quaternion rotation =
                Quaternion.LookRotation(-worldNormal, Vector3.up);

            GameObject rock = Instantiate(
                prefab,
                worldPoint,
                rotation,
                transform
            );

            // 🔥 variation visuelle naturelle
            float scale = Random.Range(scaleRange.x, scaleRange.y);
            rock.transform.localScale = Vector3.one * scale;

            rock.transform.Rotate(worldNormal, Random.Range(0f, 360f), Space.Self);
            rock.transform.Rotate(Vector3.right, Random.Range(-10f, 10f), Space.Self);
            rock.transform.Rotate(Vector3.up, Random.Range(-10f, 10f), Space.Self);
        }
    }

    Vector3 RandomPointInTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        if (r1 + r2 > 1f)
        {
            r1 = 1f - r1;
            r2 = 1f - r2;
        }

        return a +
               r1 * (b - a) +
               r2 * (c - a);
    }
}