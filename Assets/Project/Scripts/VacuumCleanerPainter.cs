using UnityEngine;

public class VacuumCleanerPainter : MonoBehaviour
{
    public Camera cam;
    public float maxDistance = 3f;
    public LayerMask cleanableMask;

    public int maskResolution = 1024;
    public float brushRadiusUV = 0.05f;
}
