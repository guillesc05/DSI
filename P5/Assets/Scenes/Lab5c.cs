using System.Collections;
using System.Collections.Generic;
using Lab5b_namespace;
using Lab5c_namespace;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab5c : MonoBehaviour
{
    List<Individuo> individuos;
    Individuo selecIndividuo;

    VisualElement tarjeta1;
    VisualElement tarjeta2;
    VisualElement tarjeta3;
    VisualElement tarjeta4;

    TextField input_nombre;
    TextField input_apellido;

    public Texture2D img1;
    public Texture2D img2;
    public Texture2D img3;


    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        tarjeta1 = root.Q("Tarjeta1");
        tarjeta2 = root.Q("Tarjeta2");
        tarjeta3 = root.Q("Tarjeta3");
        tarjeta4 = root.Q("Tarjeta4");

        input_nombre = root.Q<TextField>("InputNombre");
        input_apellido = root.Q<TextField>("InputApellido");

        individuos = BaseDatos.getData();

        VisualElement panelDcha = root.Q("Dcha");
        panelDcha.RegisterCallback<ClickEvent>(SeleccionTarjeta);

        root.Q("img1").style.backgroundImage = img1;
        root.Q("img2").style.backgroundImage = img2;
        root.Q("img3").style.backgroundImage = img3;

        VisualElement imageIz = root.Q("Izda").Q("header");
        imageIz.RegisterCallback<ClickEvent>(SeleccionImagen);

        //plantilla.RegisterCallback<ClickEvent>(SeleccionIndividuo);
        input_nombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
        input_apellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);


        InitializeUI();
    }

    void SeleccionTarjeta(ClickEvent e)
    {
        VisualElement tarjeta = e.target as VisualElement;
        selecIndividuo = tarjeta.userData as Individuo;

        input_nombre.SetValueWithoutNotify(selecIndividuo.Nombre);
        input_apellido.SetValueWithoutNotify(selecIndividuo.Apellido);
    }

    void SeleccionImagen(ClickEvent e)
    {
        var imagen = (e.target as VisualElement).style.backgroundImage.value.texture;
        Debug.Log(imagen);
        selecIndividuo.Image = imagen;
    }

    void InitializeUI()
    {
        Tarjeta tar1 = new Tarjeta(tarjeta1, individuos[0]);
        Tarjeta tar2 = new Tarjeta(tarjeta2, individuos[1]);
        Tarjeta tar3 = new Tarjeta(tarjeta3, individuos[2]);
        Tarjeta tar4 = new Tarjeta(tarjeta4, individuos[3]);
    }

    void CambioNombre(ChangeEvent<string> evt)
    {
        selecIndividuo.Nombre = evt.newValue;
    }
    void CambioApellido(ChangeEvent<string> evt)
    {

        selecIndividuo.Apellido = evt.newValue;
    }

}
