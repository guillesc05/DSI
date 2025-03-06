using UnityEngine;
using UnityEngine.UIElements;


public class Lab3 : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        VisualElement izda = root.Q("Izda");
        VisualElement dcha = root.Q("Dcha");

        izda.RegisterCallback<ClickEvent>(
            ev =>
            {
                Debug.Log("Contenedor Izquierda. Fase: " + ev.propagationPhase);
                Debug.Log("Contenedor Izquierda. Target: " + (ev.target as VisualElement).name);
                (ev.target as VisualElement).style.backgroundColor = Color.green;
            }, TrickleDown.TrickleDown);

        dcha.RegisterCallback<ClickEvent>(
            ev =>
            {
                Debug.Log("Contenedor Derecha. Fase: " + ev.propagationPhase);
                Debug.Log("Contenedor Derecha. Target: " + (ev.target as VisualElement).name);
                (ev.target as VisualElement).style.backgroundColor = Color.blue;
            }, TrickleDown.TrickleDown);
    }
}
