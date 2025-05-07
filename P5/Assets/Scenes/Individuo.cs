using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lab5b_namespace
{
    public class Individuo
    {
        public event Action Cambio;

        private string nombre;

        private StyleBackground image;

        public string Nombre
        {
            get { return nombre; }
            set { 
                if(value != nombre)
                {
                    nombre = value;
                    Cambio?.Invoke();
                }
            }
        }

        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set
            {
                if (value != apellido)
                {
                    apellido = value;
                    Cambio?.Invoke();
                }
            }
        }

        public StyleBackground Image
        {
            get { return  image; }
            set
            {
                if(value != image)
                {
                    image = value;
                    Cambio?.Invoke();
                }
            }
        }

        public Individuo(string nombre, string apellido)
        {
            this.nombre = nombre;
            this.apellido = apellido;
        }

    }
}
