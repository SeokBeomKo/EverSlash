using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public enum SurfaceType
    {
        Opaque,
        Transparent
    }
    public enum BlendMode
    {
        Alpha,
        Premultiply,
        Additive,
        Multiply
    }
    private RaycastHit[] TransparentHits;
    
    public Transform player;
    public Vector3 offset;
    MeshRenderer objectRender;
    public Material transparentMaterial;
    private void Awake() {
        offset = new Vector3(0,0.5f,0);
    }

    void Update()
    {
       //CameraRayToChar();
    }

    private void CameraRayToChar(){
        Vector3 CharPos = player.position + offset;
        float Distance = Vector3.Distance(transform.position, player.transform.position);

        Vector3 DirToCam = (transform.position - CharPos).normalized;

        TransparentHits = Physics.RaycastAll(CharPos, DirToCam, Distance);
        Debug.DrawRay(CharPos, Distance * DirToCam, Color.red,0.1f);
        for (int i = 0; i < TransparentHits.Length;i++){
            RaycastHit hit = TransparentHits[i];

            //Debug.Log(hit.collider.gameObject.transform.parent.transform.GetComponentInChildren<MeshRenderer>().name);
            objectRender = hit.collider.gameObject.transform.parent.transform.GetComponentInChildren<MeshRenderer>();
            if (objectRender != null){
                transparentMaterial = objectRender.material;
                // SetModeTransparent(objectRender);
                //transparentMaterial.SetFloat("_WorkflowMode", 0f);
                transparentMaterial.SetFloat("_Surface", 1f);
                transparentMaterial.SetFloat("_Blend", 0f);
                transparentMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                transparentMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                transparentMaterial.SetInt("_ZWrite", 0);
                transparentMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                transparentMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                transparentMaterial.SetShaderPassEnabled("ShadowCaster", false);
                
                Debug.Log("렌더러 받아옴");
            }
        }
    }

    private void SetModeTransparent(MeshRenderer renderer){
        Material originalMat = renderer.sharedMaterial;
        Material materialTrans = new Material(transparentMaterial);
        materialTrans.CopyPropertiesFromMaterial(originalMat);
        renderer.sharedMaterial = materialTrans;
    }

    
}
