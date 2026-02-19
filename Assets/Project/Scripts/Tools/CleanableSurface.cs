using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class CleanableSurface : MonoBehaviour
{
    public string surfaceName;
    public string surfaceId;

    public int maskResolution = 1024;
    private string maskPropertyName = "_CleanMask"; 

    //[SerializeField] private RawImage debugRawImage; 

    public float PercentageCleaned {  get; private set; }

    public RenderTexture MaskRT { get; private set; }
    private Renderer rend;
    private int maskPropId;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        maskPropId = Shader.PropertyToID(maskPropertyName);

        InitializeTexture();
        LoadMaskFromDisc();
        CalculateProgress();
    }

    void InitializeTexture()
    {
        if (MaskRT != null) MaskRT.Release();

        MaskRT = new RenderTexture(maskResolution, maskResolution, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear); 
        MaskRT.enableRandomWrite = false;
        MaskRT.filterMode = FilterMode.Bilinear;
        MaskRT.useMipMap = true;
        MaskRT.autoGenerateMips = false;
        MaskRT.Create();

        RenderTexture activeOld = RenderTexture.active;
        RenderTexture.active = MaskRT;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = activeOld;

        rend.material.SetTexture(maskPropId, MaskRT);

        //if (debugRawImage != null)
        //    debugRawImage.texture = MaskRT;
    }

    private void OnDestroy()
    {
        if (MaskRT != null) MaskRT.Release();
    }

    public void CalculateProgress()
    {
        MaskRT.GenerateMips();

        int mipLevel = (int)Mathf.Log(maskResolution, 2);

        AsyncGPUReadback.Request(MaskRT, mipLevel, OnCompleteReadback);
    }

    void OnCompleteReadback(AsyncGPUReadbackRequest request)
    {
        if (request.hasError) return;
        var data = request.GetData<byte>();

        if (data.Length > 0)
        {
            float averageValue = data[0] / 255f;

            PercentageCleaned = averageValue * 100f;

        }
    }

    private string GetMaskPath()
    {
        return Path.Combine(Application.persistentDataPath, $"cleanmask_{surfaceId}.png");
    }

    public void SaveMaskToDisc()
    {
        if (MaskRT == null) return;
        if (!MaskRT.IsCreated()) MaskRT.Create();

        var old = RenderTexture.active;
        RenderTexture.active = MaskRT;

        Texture2D tex = new Texture2D(MaskRT.width, MaskRT.height, TextureFormat.ARGB32, false, true);
        tex.ReadPixels(new Rect(0, 0, MaskRT.width, MaskRT.height), 0, 0);
        tex.Apply(false, false);

        RenderTexture.active = old;

        byte[] png = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes(GetMaskPath(), png);
    }

    public void LoadMaskFromDisc()
    {
        string path = GetMaskPath();
        if (!File.Exists(path)) return;

        byte[] png = File.ReadAllBytes(path);

        Texture2D tex = new Texture2D(2,2, TextureFormat.ARGB32, false, true);
        ImageConversion.LoadImage(tex, png, false);

        Graphics.Blit(tex, MaskRT);
        Destroy(tex);

        MaskRT.GenerateMips();
    }
}