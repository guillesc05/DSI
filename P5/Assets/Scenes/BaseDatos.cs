using UnityEngine;
using System.Collections.Generic;
using Lab5b_namespace;

namespace Lab5c_namespace
{
    public class BaseDatos : MonoBehaviour
    {
        public static List<Individuo> getData()
        {
            List<Individuo> datos = new List<Individuo>();

            Individuo perico = new Individuo("Perico", "Palotes");
            Individuo tornasol = new Individuo("Tornasol", "Tornasolado");
            Individuo luca = new Individuo("Luca", "Lucatelli");
            Individuo ivan = new Individuo("Ivan", "Ivanovich");

            datos.Add(perico);
            datos.Add(tornasol);
            datos.Add(luca);
            datos.Add(ivan);

            return datos;
        }
    }
}

