using UnityEngine;
using MicKami.ProceduralMotion;

public class ProceduralScaleDemo : MonoBehaviour
{
    public ProceduralMotion proceduralMotion;
    Vector3 targetScale;

    private void Update()
    {
        print(targetScale);
        if (Input.GetMouseButtonDown(0))
            targetScale += Vector3.one * 2;
        if (Input.GetMouseButtonDown(1))
            targetScale -= Vector3.one * 2;

        targetScale = Vector3.Max(targetScale, Vector3.one);
        targetScale = Vector3.Min(targetScale, Vector3.one * 7);

        var dt = Time.deltaTime;
        transform.localScale = proceduralMotion.UpdateVector3(transform.localScale, targetScale, dt);
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Press LMB and RMB to grow and shrink the ball");
        GUILayout.EndHorizontal();
    }
}
