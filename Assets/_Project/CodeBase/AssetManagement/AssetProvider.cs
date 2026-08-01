using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace _Project.CodeBase.AssetManagement
{
    public class AssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _assetRequests = new();

        public async UniTask InitializeAsync()
        {
            await Addressables.InitializeAsync().ToUniTask();
        }

        public async UniTask<TAsset> Load<TAsset>(string key) where TAsset : class
        {
            if (!_assetRequests.TryGetValue(key, out AsyncOperationHandle handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                _assetRequests.Add(key, handle);
            }

            await handle.ToUniTask();

            return handle.Result as TAsset;
        }

        public async UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class
        {
            return await Load<TAsset>(assetReference.AssetGUID);
        }

        public async UniTask<List<string>> GetAssetsListByLabel<TAsset>(string label)
        {
            return await GetAssetsListByLabel(label, typeof(TAsset));
        }

        public async UniTask<List<string>> GetAssetsListByLabel(string label, Type type = null)
        {
            AsyncOperationHandle<IList<IResourceLocation>> operationHandle = Addressables.LoadResourceLocationsAsync(label, type);

            IList<IResourceLocation> locations = await operationHandle.ToUniTask();

            List<string> assetKeys = new(locations.Count);

            foreach (IResourceLocation location in locations)
            {
                assetKeys.Add(location.PrimaryKey);
            }

            Addressables.Release(operationHandle);
            return assetKeys;
        }

        public async UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class
        {
            List<UniTask<TAsset>> tasks = new(keys.Count);

            foreach (string key in keys)
            {
                tasks.Add(Load<TAsset>(key));
            }

            return await UniTask.WhenAll(tasks);
        }

        public async UniTask WarmupAssetsByLabel(string label)
        {
            List<string> assetsList = await GetAssetsListByLabel(label);
            await LoadAll<object>(assetsList);
        }

        public async UniTask ReleaseAssetsByLabel(string label)
        {
            List<string> assetsList = await GetAssetsListByLabel(label);

            foreach (string assetKey in assetsList)
            {
                if (_assetRequests.TryGetValue(assetKey, out AsyncOperationHandle handler))
                {
                    Addressables.Release(handler);
                    _assetRequests.Remove(assetKey);
                }
            }
        }

        public void Cleanup()
        {
            foreach (KeyValuePair<string, AsyncOperationHandle> assetRequest in _assetRequests)
            {
                Addressables.Release(assetRequest.Value);
            }

            _assetRequests.Clear();
        }
    }
}