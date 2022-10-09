using System.Collections.Generic;
using System.Threading.Tasks;
using Client;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class CardsArtDownloader
{
    private const string EndPointBase = @"https://picsum.photos/v2/list?page={0}&limit={1}";

    public async Task<(Sprite, ImageData)[]> GetSprites(int count)
    {
        var page = Random.Range(0, 100);
        var endPoint = string.Format(EndPointBase, page, count);
        var texturesInfo = await GetTexturesInfo(endPoint);


        var artsData = new (Sprite, ImageData)[count];

        for (int i = 0; i < texturesInfo.Count; i++)
        {
            var req = UnityWebRequestTexture.GetTexture(texturesInfo[i].download_url);
            UnityWebRequestAsyncOperation awaiter = req.SendWebRequest();
            await AwaitTextureDownload(awaiter);
            var texture = DownloadHandlerTexture.GetContent(req);
            
            var sprRect = new Rect(0, 0, texture.width, texture.height);

            artsData[i] = (Sprite.Create(texture, sprRect, Vector2.zero), texturesInfo[i]);
        }
        
        return artsData;
    }

    private async Task AwaitTextureDownload(UnityWebRequestAsyncOperation operation)
    {
        TaskCompletionSource<bool> taskCompletionSource = new();
        operation.completed += asyncOperation => taskCompletionSource.SetResult(asyncOperation.isDone);
        await taskCompletionSource.Task;
    }
    
    private async Task<List<ImageData>> GetTexturesInfo(string endPoint)
    {
        var request = UnityWebRequest.Get(endPoint);
        UnityWebRequestAsyncOperation awaiter = request.SendWebRequest();
        await AwaitTextureDownload(awaiter);
        
        string texturesInfoText = request.downloadHandler.text;

        var texturesInfo = JsonConvert.DeserializeObject<List<ImageData>>(texturesInfoText);
        return texturesInfo;
    }
}