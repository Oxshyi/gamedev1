using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Creates text mesh with default values
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), 
                                        int fontSize = 40, Color? textColor = null, TextAnchor anchor = TextAnchor.UpperLeft, 
                                        TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000){
        if (textColor == null) textColor = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)textColor, anchor, textAlignment, sortingOrder);
    }
    // Creates a text mesh at the specified location 
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder){
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }
    // <summary> Gets the mouse world postion (Relative to the camera)
    //           casts a ray to a plane that then returns the ray hit point
    //           which is the mouse position
    // <paramerter> passed gameObjects transform 
    public static Vector3 GetMouseWorldPosition(Transform gameObjectsTransform){
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       Plane plane = new Plane(Vector3.forward, gameObjectsTransform.position);
       float dist = 0;
       if(plane.Raycast(ray, out dist)){
           Vector3 pos = ray.GetPoint(dist);
            return pos;
       }else{
           return Vector3.zero;
       }

    }
}
