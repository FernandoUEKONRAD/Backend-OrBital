using Orbital.API.Models;

namespace Orbital.API.Services
{
    /// <summary>
    /// Clase auxiliar que calcula los scores estratégicos de un planeta
    /// basado en sus características y recursos
    /// </summary>
    public class CalculadorValorEstrategico
    {
        // Constantes de ponderación (pueden ajustarse)
        private const decimal FACTOR_PRECIO_FINAL = 1.5m;

        /// <summary>
        /// Calcula el score de recursos basado en los valores unitarios y cantidades
        /// </summary>
        public decimal CalcularRecursosScore(List<RecursoPlanetario> recursos)
        {
            if (!recursos.Any())
                return 0m;

            decimal totalValor = recursos
                .Where(r => r.Extraible)
                .Sum(r => r.Valor_Unitario * r.Cantidad_Estimada);

            // Normalizar a rango 0-5 (dividir entre 100 para obtener una escala manejable)
            decimal score = Math.Min((totalValor / 100), 5m);
            return Math.Round(score, 2);
        }

        /// <summary>
        /// Calcula el score de poder basado en el nivel de vida nativa
        /// Rango: 0-5
        /// </summary>
        public decimal CalcularPoderScore(string nivelVidaNativa)
        {
            return nivelVidaNativa?.ToLower() switch
            {
                "sin vida" => 0m,
                "primitiva" => 1m,
                "bajo" => 2m,
                "medio" => 3m,
                "alto" => 4m,
                "avanzado" => 5m,
                _ => 0m
            };
        }

        /// <summary>
        /// Calcula el score de dificultad de conquista basado en el poder
        /// Clasificaciones: Bajo (1-2), Medio (2.5-3), Alto (4-5)
        /// </summary>
        public decimal CalcularDificultadScore(decimal poderScore)
        {
            return poderScore switch
            {
                <= 2m => 1m,      // Bajo
                <= 3m => 2.5m,    // Medio
                _ => 4m            // Alto
            };
        }

        /// <summary>
        /// Calcula el score de tecnología basado en nivel_tecnologico del planeta
        /// Rango: 1-10 se mapea a 0-5
        /// </summary>
        public decimal CalcularTecnologiaScore(int nivelTecnologico)
        {
            // Normalizar 1-10 a 0-5
            decimal score = ((nivelTecnologico - 1) / 9m) * 5m;
            return Math.Round(score, 2);
        }

        /// <summary>
        /// Calcula el score de ubicación (MVP: simplificado a 2.5 - ubicación estratégica media)
        /// En futuras versiones: calcular distancia a otros planetas estratégicos
        /// </summary>
        public decimal CalcularUbicacionScore()
        {
            // MVP: valor fijo. En v2 calcular distancia euclidiana 3D
            return 2.5m;
        }

        /// <summary>
        /// Calcula el score de riesgo (amenazas detectadas, civilizaciones hostiles)
        /// MVP: 0 si no hay amenazas, 3 si las hay
        /// </summary>
        public decimal CalcularRiesgoScore(bool tieneAmenazas = false)
        {
            return tieneAmenazas ? 3m : 0m;
        }

        /// <summary>
        /// Calcula el valor total como promedio ponderado de los 5 scores
        /// Fórmula: (Recursos + Poder + Tecnologia + Ubicacion + Riesgo) / 5 * 100
        /// </summary>
        public decimal CalcularValorTotal(
            decimal recursosScore,
            decimal poderScore,
            decimal tecnologiaScore,
            decimal ubicacionScore,
            decimal riesgoScore)
        {
            decimal promedio = (recursosScore + poderScore + tecnologiaScore + ubicacionScore + riesgoScore) / 5m;
            decimal valorTotal = promedio * 100m; // Escalar a rango 0-500
            return Math.Round(valorTotal, 2);
        }

        /// <summary>
        /// Clasifica el planeta según su valor total
        /// A: 800+, B: 600-799, C: 400-599, D: < 400
        /// </summary>
        public string CalcularClasePlaneta(decimal valorTotal)
        {
            return valorTotal switch
            {
                >= 400m => "A",
                >= 300m => "B",
                >= 200m => "C",
                _ => "D"
            };
        }

        /// <summary>
        /// Calcula el precio final del planeta
        /// Fórmula: valor_total * FACTOR_PRECIO_FINAL
        /// </summary>
        public decimal CalcularPrecioFinal(decimal valorTotal)
        {
            decimal precioFinal = valorTotal * FACTOR_PRECIO_FINAL;
            return Math.Round(precioFinal, 2);
        }
    }
}
