using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
		public string tag;
        public GameObject audioSourceHolder;
        public int size;
    }

	#region Singleton

	public static AudioPooler Instance;

	private void Awake()
	{
		Instance = this;
	}

	#endregion

	public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
			Queue<GameObject> audioPool = new Queue<GameObject>();
			for (int i = 0; i < pool.size; i++)
			{
				GameObject audioHolder = Instantiate(pool.audioSourceHolder);
				audioHolder.SetActive(false);
				audioPool.Enqueue(audioHolder);
			}

			poolDictionary.Add(pool.tag, audioPool);
        }
    }

	public GameObject SpawnFromPool(string tag)
	{
		if(!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
			return null;
		}

		GameObject objectToSpawn = poolDictionary[tag].Dequeue();
		objectToSpawn.SetActive(true);

		poolDictionary[tag].Enqueue(objectToSpawn);

		return objectToSpawn;
	}
}
