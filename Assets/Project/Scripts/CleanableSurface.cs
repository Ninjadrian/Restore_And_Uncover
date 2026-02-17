using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CleanableSurface : MonoBehaviour
{
    public string surfaceName;

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
}