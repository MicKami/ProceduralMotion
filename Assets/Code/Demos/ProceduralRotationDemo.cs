using UnityEngine;
using MicKami.ProceduralMotion;

public class ProceduralRotationDemo : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] ProceduralMotion proceduralMotion;
    [SerializeField] float speed;
    private void Update()
    {
        var dt = Time.deltaTime;
        float input_horizontal = Input.GetAxis("Horizontal");
        float input_vertical = Input.GetAxis("Vertical");
        Vector3 inputVector = new Vector3(input_vertical, -input_horizontal, 0).normalized;
        target.Rotate(inputVector * speed * dt, Space.World);

        transform.rotation = proceduralMotion.UpdateQuaternion(transform.rotation, target.rotation, dt);
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Press WASD keys to rotate the ball");
        GUILayout.EndHorizontal();
    }
}
