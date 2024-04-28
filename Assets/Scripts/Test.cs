using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("64ee0a4463fdfdc41ac7a06c8f5f2f0f");
        handle.Completed += OnHatLoadComplete;
    }

    private void OnHatLoadComplete(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(asyncOperationHandle.Result);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
