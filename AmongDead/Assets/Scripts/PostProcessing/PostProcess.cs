using UnityEngine;


[ExecuteInEditMode]
public class PostProcess : MonoBehaviour
{
    public Material CameraMainPostProcessMat;

    public void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, CameraMainPostProcessMat);
    }
}
