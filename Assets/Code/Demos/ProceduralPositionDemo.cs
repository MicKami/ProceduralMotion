using UnityEngine;
using MicKami.ProceduralMotion;

public class ProceduralPositionDemo : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] ProceduralMotion proceduralMotion;
    [SerializeField] float min_X;
    [SerializeField] float max_X;
    [SerializeField] float speed;

    private void Update()
    {
        var dt = Time.deltaTime;
        float input = Input.GetAxis("Horizontal");
        Vector3 inputVector = target.position;
        inputVector.x += input * dt * speed;
        inputVector.x = Mathf.Clamp(inputVector.x, min_X, max_X);
        target.position = inputVector;


        Vector3 targetPositon = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = proceduralMotion.UpdateVector3(transform.position, targetPositon, dt);
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("Press AD keys to move the ball");
        GUILayout.EndHorizontal();
    }
}
