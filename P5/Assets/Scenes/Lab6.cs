using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lab6
{
    public class Lab6 : MonoBehaviour
    {
        VisualElement botonCrear, botonGuardar, contenedorDerecho;
        Toggle toggleModificar;

        TextField inputNombre, inputApellido;

        Individuo individuoSelec;
        List<Individuo> _individuos = new List<Individuo>();

        [SerializeField] string fileRoute;

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            botonCrear = root.Q<VisualElement>("BotonCrear");
            botonGuardar = root.Q<VisualElement>("BotonGuardar");
            contenedorDerecho = root.Q<VisualElement>("Derecha");
            inputNombre = root.Q<TextField>("InputNombre");
            inputApellido = root.Q<TextField>("InputApellido");
            toggleModificar = root.Q<Toggle>("ToggleModificar");

            botonCrear.RegisterCallback<ClickEvent>(NuevaTarjeta);
            botonGuardar.RegisterCallback<ClickEvent>(GuardarEnJson);
            inputNombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
            inputApellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);
            contenedorDerecho.RegisterCallback<ClickEvent>(SeleccionTarjeta);
        }

        void NuevaTarjeta(ClickEvent evt)
        {
            if (toggleModificar.value) return;

            VisualTreeAsset plantilla = Resources.Load<VisualTreeAsset>("P6/Tarjeta");
            VisualElement tarjetaVE = plantilla.Instantiate();

            contenedorDerecho.Add(tarjetaVE);

            Individuo individuo = new Individuo(inputNombre.value, inputApellido.value);
            Tarjeta tarjeta = new Tarjeta(tarjetaVE, individuo);

            individuoSelec = individuo;

            _individuos.Add(individuo);
            string jsonIndividuo = JsonHelperIndividuo.ToJson(_individuos, true);
            Debug.Log(jsonIndividuo);
            
        }

        void CambioNombre(ChangeEvent<string> evt)
        {
            if (toggleModificar.value) individuoSelec.Nombre = evt.newValue;
        }

        void CambioApellido(ChangeEvent<string> evt)
        {
            if(toggleModificar.value) individuoSelec.Apellido = evt.newValue;
        }

        void SeleccionTarjeta(ClickEvent evt)
        {
            VisualElement tarjeta = evt.target as VisualElement;
            individuoSelec = tarjeta.userData as Individuo;

            inputNombre.SetValueWithoutNotify(individuoSelec.Nombre);
            inputApellido.SetValueWithoutNotify(individuoSelec.Apellido);

            toggleModificar.value = true;

            TarjetaNegro();
            TarjetaBlanco(tarjeta);
        }

        void GuardarEnJson(ClickEvent evt)
        {
            StreamWriter sr = new StreamWriter(fileRoute);

            string json = JsonHelperIndividuo.ToJson(_individuos);
            sr.Write(json);

            sr.Close();
        }

        void LeerDeJSON(ClickEvent evt)
        {
            StreamReader sr = new StreamReader(fileRoute);

            _individuos = JsonHelperIndividuo.FromJson<Individuo>(sr.ReadToEnd());

            sr.Close();
        }

        void TarjetaNegro()
        {
            List<VisualElement> tarjetas = contenedorDerecho.Children().ToList();

            foreach (var tarjeta in tarjetas)
            {
                VisualElement body = tarjeta.Q("Tarjeta");

                body.style.borderBottomColor = Color.black;
                body.style.borderTopColor = Color.black;
                body.style.borderLeftColor = Color.black;
                body.style.borderRightColor = Color.black;
            }
        }

        void TarjetaBlanco(VisualElement tarjeta)
        {
            VisualElement body = tarjeta.Q("Tarjeta");

            body.style.borderBottomColor = Color.white;
            body.style.borderTopColor = Color.white;
            body.style.borderLeftColor = Color.white;
            body.style.borderRightColor = Color.white;
        }
    }
}
