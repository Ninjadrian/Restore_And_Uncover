using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class CleanableSurface : MonoBehaviour
{
    public string surfaceName;
    public string surfaceId;

    public int maskResolution = 1024;
    private string maskPropertyName = "_CleanMask";

    public float maximumUVCoverage = 100;

    //[SerializeField] private RawImage debugRawImage; 

    public float percentageCleaned {  get; private set; }

    public RenderTexture MaskRT { get; private set; }
    private Renderer rend;
    private int maskPropId;

    private bool isDestroyed;

    private void Awake()
    {
        //Si la superficie ya fue limpiada no debe aparecer
        if (PlayerProfiler.Instance != null && PlayerProfiler.Instance.IsSurfaceCleaned(surfaceId))
        {
            Destroy(gameObject);
            return;
        }

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
        isDestroyed = true;

        if (MaskRT == null) return;

        if (RenderTexture.active == MaskRT)
            RenderTexture.active = null;

        //Libera recursos de GPU
        MaskRT.Release();

        //Destruye el objeto RT
        Destroy(MaskRT);
        MaskRT = null;

        LevelCompletionManager.Instance.TryCompleteLevel();
    }

    public void CalculateProgress()
    {
        MaskRT.GenerateMips();

        int mipLevel = (int)Mathf.Log(maskResolution, 2);

        AsyncGPUReadback.Request(MaskRT, mipLevel, OnCompleteReadback);
    }

    void OnCompleteReadback(AsyncGPUReadbackRequest request)
    {
        if (isDestroyed) return;
        if (request.hasError) return;
        var data = request.GetData<byte>();

        if (data.Length > 0)
        {
            float averageValue = data[0] / 255f * 100f;

            percentageCleaned = Mathf.Clamp01(averageValue/ maximumUVCoverage);

            Debug.Log("Porcentaje de "+surfaceName + ": " + percentageCleaned);

            if (percentageCleaned > 0.96f) 
            {
                
                percentageCleaned = 1;

                PlayerProfiler.Instance.MarkSurfaceCleaned(surfaceId);


                Destroy(gameObject);
            }
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