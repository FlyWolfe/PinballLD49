using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraShenanigans : MonoBehaviour
{
    public Camera secondCamera;

    void OnEnable()
    {
        //RenderPipelineManager.beginFrameRendering += RenderPipelineManager_beginCameraRendering;
        //RenderPipelineManager.beginCameraRendering += RenderPipelineManager_beginCameraRendering;
        //RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }
    void OnDisable()
    {
        //RenderPipelineManager.beginFrameRendering += RenderPipelineManager_beginCameraRendering;
        //RenderPipelineManager.beginCameraRendering -= RenderPipelineManager_beginCameraRendering;
        //RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }
    private void RenderPipelineManager_beginCameraRendering(ScriptableRenderContext context, Camera[] cameras)
    {
        //secondCamera.enabled = true;
        //Camera.main.Render();
        //secondCamera.Render();
    }
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        //secondCamera.enabled = false;
        //secondCamera.Render();
    }
    
}
