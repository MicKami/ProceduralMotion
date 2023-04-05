using UnityEngine;
using UnityEngine.UI;
using MicKami.ProceduralMotion;

public class ProceduralUIDemo : MonoBehaviour
{
    [SerializeField] ProceduralMotion proceduralMotion;
    [SerializeField] Slider targetSlider;
    [SerializeField] Slider slider;
    private float target;

    private void Update()
    {
        target = targetSlider.value;
        float dt = Time.deltaTime;
        slider.value = proceduralMotion.UpdateFloat(slider.value, target, dt);
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Use the bottom slider to move the top one");
        GUILayout.EndHorizontal();
    }
}
