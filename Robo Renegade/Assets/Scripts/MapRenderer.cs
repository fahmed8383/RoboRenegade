using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour
{

    public Transform player;
    public List<GameObject> chunks;
    public GameObject consumeables;

    private const int chunkSize = 26;

    private int prevXChunk = 1000;
    private int prevYChunk = 1000;

    private Hashtable chunksCreated = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = player.position;
        CreateChunk(pos);
    }

    // Update is called once per frame
    void Update()
    {
        int currentXChunk = Mathf.FloorToInt(player.position.x / chunkSize) * chunkSize;
        int currentYChunk = Mathf.FloorToInt(player.position.y / chunkSize) * chunkSize;

        if (currentXChunk != prevXChunk || currentYChunk != prevYChunk)
        {
            HideChunks(prevXChunk, prevYChunk);
            RenderChunks(currentXChunk, currentYChunk);
            prevXChunk = currentXChunk;
            prevYChunk = currentYChunk;
        }
    }

    private void CreateChunk(Vector3 pos)
    {
        GameObject newChunk = Instantiate(chunks[Mathf.RoundToInt(Random.Range(0, chunks.Count))], pos, Quaternion.Euler(0, 0, 0), this.transform);
        if (Random.value <= 1)
        {
            Instantiate(consumeables, newChunk.transform);
        }
        chunksCreated.Add(pos, newChunk);
    }

    private void RenderChunks(int currentXChunk, int currentYChunk)
    {
        for(int x = currentXChunk-chunkSize; x <= currentXChunk+chunkSize; x += chunkSize)
        {
            for(int y = currentYChunk-chunkSize; y <= currentYChunk + chunkSize; y += chunkSize)
            {
                Vector3 coords = new Vector3(x, y, 0);
                if (!chunksCreated.ContainsKey(coords))
                {
                    CreateChunk(coords);
                } else
                {
                    GameObject currChunk = chunksCreated[coords] as GameObject;
                    currChunk.SetActive(true);
                }
            }
        }
    }

    private void HideChunks(int currentXChunk, int currentYChunk)
    {
        for (int x = currentXChunk - chunkSize; x <= currentXChunk + chunkSize; x += chunkSize)
        {
            for (int y = currentYChunk - chunkSize; y <= currentYChunk + chunkSize; y += chunkSize)
            {
                Vector3 coords = new Vector3(x, y, 0);
                if (chunksCreated.ContainsKey(coords))
                {
                    GameObject currChunk = chunksCreated[coords] as GameObject;
                    currChunk.SetActive(false);
                }
            }
        }
    }
}
