using UnityEditor;
using UnityEngine;
using MicKami.ProceduralMotion;
using static UnityEngine.Mathf;

[CustomPropertyDrawer(typeof(ProceduralMotion))]
public class ProceduralMotionDrawer : PropertyDrawer
{
    const int GRAPH_HEIGHT = 100;
    const int GRAPH_MARGIN_Y = 10;
    const int GRAPH_MARGIN_X = 20;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);
        float lineHeight = EditorGUIUtility.singleLineHeight;
        Rect boxRect = new Rect(position.x, position.y + lineHeight, position.width, position.height - lineHeight);
        GUI.Box(boxRect, GUIContent.none);

        if (property.isExpanded)
        {
            Rect graphRect = new Rect(position.x + GRAPH_MARGIN_X, position.y + position.height - GRAPH_HEIGHT, position.width - GRAPH_MARGIN_X, GRAPH_HEIGHT - GRAPH_MARGIN_Y);
            ProceduralMotion proceduralMotion = ((ProceduralMotion)property.boxedValue).Copy();
            DrawGraph(graphRect, proceduralMotion);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
            return EditorGUI.GetPropertyHeight(property) + GRAPH_HEIGHT + 2 * GRAPH_MARGIN_Y;
        return EditorGUI.GetPropertyHeight(property);
    }

    private void DrawGraph(Rect graphRect, ProceduralMotion proceduralMotion)
    {
        Vector3[] points = new Vector3[(int)graphRect.width];
        float current = 0;
        float dx = 1f / graphRect.width;
        float y_min = 0;
        float y_max = 1;
        if (proceduralMotion.F != 0)
        {
            for (int i = 0; i < points.Length; i++)
            {
                current = proceduralMotion.UpdateFloat(current, 1f, dx);
                float x = i / graphRect.width;
                float y = current;
                y_min = Min(y_min, y);
                y_max = Max(y_max, y);
                points[i] = new Vector3(x, y, 0);
            }
        }

        for (int i = 0; i < points.Length; i++)
        {
            float y = InverseLerp(y_min, y_max, points[i].y);
            points[i] = GetRectPoint(new Vector2(points[i].x, y), graphRect);
        }
        Handles.color = Color.gray;
        Handles.Label(new Vector3(graphRect.x - 12, graphRect.y + graphRect.height * InverseLerp(y_max, y_min, 0f) - 8, 0f), "0");
        Handles.Label(new Vector3(graphRect.x - 12, graphRect.y + graphRect.height * InverseLerp(y_max, y_min, 1f) - 8, 0f), "1");
        Handles.DrawLine(GetRectPoint(new Vector2(0, 0), graphRect), GetRectPoint(new Vector2(0, 1), graphRect));
        Handles.DrawLine(GetRectPoint(new Vector2(0, InverseLerp(y_min, y_max, 0f)), graphRect), GetRectPoint(new Vector2(1, InverseLerp(y_min, y_max, 0f)), graphRect));
        Handles.color = Color.green;
        Handles.DrawLine(GetRectPoint(new Vector2(0, InverseLerp(y_min, y_max, 1f)), graphRect), GetRectPoint(new Vector2(1, InverseLerp(y_min, y_max, 1f)), graphRect));
        Handles.color = Color.red;
        Handles.DrawAAPolyLine(points);
    }

    Vector3 GetRectPoint(Vector2 position, Rect rect)
    {
        float x = LerpUnclamped(rect.x, rect.x + rect.width, position.x);
        float y = LerpUnclamped(rect.y + rect.height, rect.y, position.y);
        return new Vector3(x, y, 0);
    }
}