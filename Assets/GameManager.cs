using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI FpsText;

    void Start()
    {
        Application.targetFrameRate = 60;
    }


    private void Update()
    {
        int fpsVal = Mathf.RoundToInt((1 / Time.deltaTime));
        FpsText.text = fpsVal.ToString();
    }


    public void ApplyTransparancy(GameObject _body, bool isTransparent)
    {
        var material = _body.GetComponent<MeshRenderer>().material;

        if (isTransparent)
        {
            material.SetColor("_Color", new Color(1, 1, 1, 0));
            material.SetFloat("_Glossiness", 1f);
            material.SetFloat("_Metallic", 0.75f);

            material.SetOverrideTag("RenderType", "Transparent");
            material.SetFloat("_SrcBlend", (float)BlendMode.One);
            material.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
            material.SetFloat("_ZWrite", 0.0f);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        }
        else
        {
            material.SetColor("_Color", new Color(0, 1, 0.0219624f, 1));
            material.SetFloat("_Glossiness", 0);
            material.SetFloat("_Metallic", 0.4f);

            material.SetOverrideTag("RenderType", "");
            material.SetFloat("_SrcBlend", (float)BlendMode.One);
            material.SetFloat("_DstBlend", (float)BlendMode.Zero);
            material.SetFloat("_ZWrite", 1.0f);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }


    }
}
