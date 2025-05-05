using UnityEngine;
using System.Collections.Generic;

public class CameraObstructionFader : MonoBehaviour
{
    public Transform player;
    public LayerMask obstructionMask;
    public float fadeAlpha = 0.3f;
    public float fadeDuration = 1f;

    private class FadeState
    {
        public Renderer renderer;
        public float currentAlpha;
        public float targetAlpha;
        public float fadeTime;
        public Material[] materials;

        public FadeState(Renderer rend, float startAlpha, float targetAlpha, Material[] mats)
        {
            renderer = rend;
            currentAlpha = startAlpha;
            this.targetAlpha = targetAlpha; 
            fadeTime = 0f;
            materials = mats;
        }
    }

    private Dictionary<Renderer, FadeState> fadingObjects = new Dictionary<Renderer, FadeState>();

    void LateUpdate()
    {
        HashSet<Renderer> currentObstructions = new HashSet<Renderer>();

        Vector3 dir = player.position - transform.position;
        float dist = dir.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, dir.normalized, dist, obstructionMask);
        foreach (var hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                currentObstructions.Add(rend);
                if (!fadingObjects.ContainsKey(rend))
                {
                    // Start fading out
                    fadingObjects[rend] = new FadeState(rend, GetAlpha(rend), fadeAlpha, rend.materials);
                }
                else
                {
                    // Already fading, just update target
                    fadingObjects[rend].targetAlpha = fadeAlpha;
                    fadingObjects[rend].fadeTime = 0f;
                }
            }
        }

        // Set objects that are no longer obstructions to fade in
        List<Renderer> toRemove = new List<Renderer>();
        foreach (var kvp in fadingObjects)
        {
            Renderer rend = kvp.Key;
            FadeState state = kvp.Value;
            if (!currentObstructions.Contains(rend))
            {
                state.targetAlpha = 1f;
                state.fadeTime = 0f;
            }
        }

        // Update all fading objects
        foreach (var kvp in fadingObjects)
        {
            FadeState state = kvp.Value;
            state.fadeTime += Time.deltaTime;
            float t = Mathf.Clamp01(state.fadeTime / fadeDuration);
            state.currentAlpha = Mathf.Lerp(GetAlpha(state.renderer), state.targetAlpha, t);
            SetAlpha(state.renderer, state.currentAlpha);

            // If fade is complete and fully opaque, remove from tracking
            if (Mathf.Approximately(state.currentAlpha, 1f) && Mathf.Approximately(state.targetAlpha, 1f))
                toRemove.Add(state.renderer);
        }
        foreach (var rend in toRemove)
            fadingObjects.Remove(rend);
    }

    float GetAlpha(Renderer rend)
    {
        // Use first material's color as reference
        return rend.materials[0].color.a;
    }

    void SetAlpha(Renderer rend, float alpha)
    {
        foreach (var mat in rend.materials)
        {
            Color c = mat.color;
            c.a = alpha;
            mat.color = c;
            if (alpha < 1f)
            {
                mat.SetFloat("_Mode", 2); // Transparent
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
            else
            {
                mat.SetFloat("_Mode", 0); // Opaque
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = -1;
            }
        }
    }
}