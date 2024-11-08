using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Spawner : MonoBehaviour
{
    [SerializeField] private AssetReference obj;
    private GameObject spawmedObject;
    private bool isReady;

    private void Start()
    {
        Addressables.ClearDependencyCacheAsync("eb26a2b4ce3fcb044b728c90699534d5");
        isReady = false;
        Addressables.InitializeAsync().Completed += OnAddressableInitialized;
    }

    void Update()
    {
        if (isReady == false) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnGameObject();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Addressables.ReleaseInstance(spawmedObject);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadScene();
        }
    }

    private void OnAddressableInitialized(AsyncOperationHandle<IResourceLocator> locator)
    {
        if(locator.Status == AsyncOperationStatus.Succeeded)
        {
            StartCoroutine(CheckForUpdateAndDownload());
        }
    }

    private void SpawnGameObject()
    {
        var startTime = Time.time;
        var handle = Addressables.LoadAssetAsync<GameObject>("eb26a2b4ce3fcb044b728c90699534d5");
        handle.Completed += (task) =>
        {
            var completedTime = Time.time;
            Debug.Log("Time taken " + (completedTime - startTime));
            Instantiate(task.Result);
        };
    }

    private void LoadScene()
    {
        Addressables.LoadSceneAsync("Assets/Scenes/New Scene.unity");
    }

    private void ReadAllKeys()
    {
        foreach (var locator in Addressables.ResourceLocators)
        {
            foreach (var key in locator.Keys)
            {
                if (locator.Locate(key, typeof(Object), out var locations))
                {
                    foreach(var location in locations)
                    {
                        Debug.Log($"Key: {key}, First Location: {location.PrimaryKey}, Current Location: {location.InternalId}");
                    }
                }
            }
        }
    }

    private IEnumerator CheckForUpdateAndDownload()
    {
        while(Caching.ready == false) yield return null;
        var check = Addressables.CheckForCatalogUpdates();
        check.Completed += CheckUpdateCompleted;
    }

    private void CheckUpdateCompleted(AsyncOperationHandle<List<string>> arg)
    {
        if (arg.Result.Count > 0) Addressables.UpdateCatalogs(true, arg.Result);
        isReady = true;

        //Debug.Log(Addressables.GetDownloadSizeAsync("eb26a2b4ce3fcb044b728c90699534d5").Result);
        //Addressables.DownloadDependenciesAsync("eb26a2b4ce3fcb044b728c90699534d5", true).Completed += (task) =>
        //{
        //    Debug.Log(Addressables.GetDownloadSizeAsync("eb26a2b4ce3fcb044b728c90699534d5").Result);
        //};
    }

}
