using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VacuumCleanerPainter : MonoBehaviour
{
    public TMP_Text surfaceText;
    public Image imageSlider;

    public Camera cam;
    public float maxDistance = 3f;
    public LayerMask cleanableMask;

    public int maskResolution = 1024;

    public float brushRadiusPixels = 24f;
    [Range(0f, 1f)] public float hardness = 0.4f;
    [Range(0f, 1f)] public float strength = 0.25f;

    public Shader brushShader;

    private Material brushMat;

    private static readonly int CenterRadiusId = Shader.PropertyToID("_CenterRadius");
    private static readonly int StrenghtId = Shader.PropertyToID("_Strength");

    private void Awake()
    {
        if (cam == null) cam = Camera.main;

        if (brushShader != null)
        {
            brushMat = new Material(brushShader);
        }
    }

    private void OnDestroy()
    {
        if (brushMat != null) Destroy(brushMat);
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance, cleanableMask)) return;

        var surface = hit.collider.GetComponentInParent<CleanableSurface>();
        if (surface == null) return;

        Vector2 uv = hit.textureCoord;

        //Forzar UV dentro de 0 y 1
        uv.x = Mathf.Repeat(uv.x, 1f);
        uv.y = Mathf.Repeat(uv.y, 1f);

        Paint(surface.MaskRT, uv);

        surface.CalculateProgress();

        surfaceText.text = surface.surfaceName;

        imageSlider.fillAmount = Mathf.Lerp(imageSlider.fillAmount, surface.percentageCleaned, Time.deltaTime * 5f);
    }

    private void Paint(RenderTexture maskRT, Vector2 uv)
    {
        if (maskRT == null) return;
        if (!maskRT.IsCreated()) maskRT.Create();

        float radiusUV = brushRadiusPixels / (float)maskRT.width;

        brushMat.SetVector(CenterRadiusId, new Vector4(uv.x, uv.y, radiusUV, hardness));
        brushMat.SetFloat(StrenghtId, strength);

        RenderTexture tmp = RenderTexture.GetTemporary(maskRT.width, maskRT.height, 0, maskRT.format, RenderTextureReadWrite.Linear);

        Graphics.Blit(maskRT, tmp, brushMat);
        Graphics.Blit(tmp, maskRT);

        RenderTexture.ReleaseTemporary(tmp);
    }
}