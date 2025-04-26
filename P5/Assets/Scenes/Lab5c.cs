using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lab6;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab5c : MonoBehaviour
{
    Individuo selecIndividuo;

    TextField input_nombre;
    TextField input_apellido;

    public Texture2D img1;
    public Texture2D img2;
    public Texture2D img3;

    [SerializeField] string fileRoute;

    [SerializeField] VisualTreeAsset plantilla;

    List<Individuo> individuos = new List<Individuo>();

    VisualElement contenedorTarjeta;


    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        contenedorTarjeta = root.Q("Dcha");

        input_nombre = root.Q<TextField>("InputNombre");
        input_apellido = root.Q<TextField>("InputApellido");

        LeerDeJSON();

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
        foreach(var individuo in individuos)
        {
            VisualElement tarjetaVE = plantilla.Instantiate();
            contenedorTarjeta.Add(tarjetaVE);
            Tarjeta t = new Tarjeta(tarjetaVE, individuo);
        }
    }

    void CambioNombre(ChangeEvent<string> evt)
    {
        selecIndividuo.Nombre = evt.newValue;
    }
    void CambioApellido(ChangeEvent<string> evt)
    {

        selecIndividuo.Apellido = evt.newValue;
    }

    void GuardarEnJson(ClickEvent evt)
    {
        StreamWriter sr = new StreamWriter(fileRoute);

        string json = JsonHelperIndividuo.ToJson(individuos);
        sr.Write(json);

        sr.Close();
    }

    void LeerDeJSON()
    {
        StreamReader sr = new StreamReader(fileRoute);

        individuos = JsonHelperIndividuo.FromJson<Individuo>(sr.ReadToEnd());

        sr.Close();
    }

}
