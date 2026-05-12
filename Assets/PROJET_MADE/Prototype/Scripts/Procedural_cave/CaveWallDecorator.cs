using UnityEngine;

public class CaveWallDecorator : MonoBehaviour
{
    [Header("References")]
    public MeshFilter meshFilter;

    [Header("Rock Prefabs")]
    public GameObject[] rockPrefabs;

    [Header("Spawn Settings")]
    [Range(0f, 1f)]
    public float spawnChance = 0.15f;

    public float offsetFromWall = 0.15f;

    [Header("Scale")]
    public Vector2 randomScaleRange = new Vector2(0.8f, 2f);

    [Header("Wall Detection")]
    [Range(0f, 1f)]
    public float maxUpDot = 0.4f;

    void Start()
    {
        SpawnRocksOnWalls();
    }

    void SpawnRocksOnWalls()
    {
        Mesh mesh = meshFilter.mesh;

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Vertices du triangle
            Vector3 v1 = transform.TransformPoint(vertices[triangles[i]]);
            Vector3 v2 = transform.TransformPoint(vertices[triangles[i + 1]]);
            Vector3 v3 = transform.TransformPoint(vertices[triangles[i + 2]]);

            // Normale du triangle
            Vector3 normal = Vector3.Cross(v2 - v1, v3 - v1).normalized;

            // Détection des murs
            float upDot = Mathf.Abs(Vector3.Dot(normal, Vector3.up));

            // Ignore sol/plafond
            if (upDot > maxUpDot)
                continue;

            // Chance de spawn
            if (Random.value > spawnChance)
                continue;

            // Centre du triangle
            Vector3 center = (v1 + v2 + v3) / 3f;

            // Décale légèrement hors du mur
            Vector3 spawnPos = center + normal * offsetFromWall;

            // Choisit un prefab aléatoire
            GameObject prefab =
                rockPrefabs[Random.Range(0, rockPrefabs.Length)];

            // Rotation selon la normale
            Quaternion rotation =
                Quaternion.LookRotation(normal);

            // Rotation aléatoire supplémentaire
            rotation *= Quaternion.Euler(
                Random.Range(-20f, 20f),
                Random.Range(0f, 360f),
                Random.Range(-20f, 20f)
            );

            // Spawn
            GameObject rock = Instantiate(
                prefab,
                spawnPos,
                rotation,
                transform
            );

            // Scale aléatoire
            float randomScale =
                Random.Range(
                    randomScaleRange.x,
                    randomScaleRange.y
                );

            rock.transform.localScale *= randomScale;
        }
    }
}