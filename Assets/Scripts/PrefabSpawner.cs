using UnityEngine;

//arý ve yaprak objelerini oyunun baþlangýç pozisyonlarýnda oluþturma
public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] GameObject beePrefab;
    [SerializeField] GameObject leafPrefab;
    [SerializeField] GameObject flowerPrefab;

    GameObject spawnedBee;

    void Start()
    {
        spawnBeeWithLeaf();
    }

    void spawnBeeWithLeaf()
    {
        Vector3 sceneCenter = Camera.main.transform.position;
        float offsetY = -2.5f; //scene'in ortasýndan ne kadar altta spawnlanacak
        Vector3 spawnPosition = new Vector3(sceneCenter.x, sceneCenter.y + offsetY, 0f);        
        GameObject leaf = Instantiate(leafPrefab, spawnPosition, Quaternion.identity);

        Collider2D leafCollider = leaf.GetComponent<Collider2D>();
        Vector3 leafTop = leafCollider.bounds.max;        
        
        Vector3 beeSpawnPosition = new Vector3(leafTop.x - 1.4f, leafTop.y + 1f, leafTop.z);
        spawnedBee = Instantiate(beePrefab, beeSpawnPosition, Quaternion.identity);
        
        Vector3 flowerSpawnPosition = new Vector3(leafTop.x + 0.025f, leafTop.y + 0.65f, leafTop.z);
        Instantiate(flowerPrefab, flowerSpawnPosition, Quaternion.identity);
    }

    public GameObject GetSpawnedBee()
    {
        return spawnedBee;
    }

}