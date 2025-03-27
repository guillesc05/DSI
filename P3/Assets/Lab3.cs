using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class Lab3 : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        VisualElement izda = root.Q("Izda");
        VisualElement dcha = root.Q("Dcha");

        List<VisualElement> lveizda = izda.Children().ToList();
        List<VisualElement> lvedcha = dcha.Children().ToList();

        lveizda.ForEach(elem=> elem.Children().ToList().ForEach(elemI => elemI.AddManipulator(new ExampleResizer())));
        lveizda.ForEach(elem=> elem.Children().ToList().ForEach(elemI => elemI.AddManipulator(new HoverManipulator())));
        lvedcha.ForEach(elem => elem.Children().ToList().ForEach(elemI => elemI.AddManipulator(new ExampleDragger())));
        lvedcha.ForEach(elem => elem.Children().ToList().ForEach(elemI => elemI.AddManipulator(new HoverManipulator())));

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
