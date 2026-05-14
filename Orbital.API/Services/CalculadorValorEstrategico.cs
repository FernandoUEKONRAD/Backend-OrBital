using Orbital.API.Models;

namespace Orbital.API.Services
{
    public class CalculadorValorEstrategico
    {
        private const decimal FACTOR_PRECIO_FINAL = 1.5m;

        /// <summary>
        /// Score de recursos basado en valor total extraíble. Rango: 0-5.
        /// </summary>
        public decimal CalcularRecursosScore(List<RecursoPlaneta> recursos)
        {
            if (!recursos.Any())
                return 0m;

            decimal totalValor = recursos
                .Where(r => r.Extraible)
                .Sum(r => r.Valor_Unitario * r.Cantidad_Estimada);

            decimal score = Math.Min((totalValor / 100_000_000m) * 5m, 5m);
            return Math.Round(score, 2);
        }

        /// <summary>
        /// Score de poder nativo según nivel de vida. Rango: 0-5.
        /// </summary>
        public decimal CalcularPoderScore(string nivelVidaPlaneta)
        {
            return nivelVidaPlaneta?.ToLower() switch
            {
                "sin vida"  => 0m,
                "primitiva" => 1m,
                "bajo"      => 2m,
                "medio"     => 3m,
                "alto"      => 4m,
                "avanzado"  => 5m,
                _           => 0m
            };
        }

        /// <summary>
        /// Score de dificultad de conquista derivado del poder nativo. Rango: 1-4.
        /// </summary>
        public decimal CalcularDificultadScore(decimal poderScore)
        {
            return poderScore switch
            {
                <= 2m => 1m,
                <= 3m => 2.5m,
                _     => 4m
            };
        }

        /// <summary>
        /// Score de tecnología para nivel 1-4 (Primitivo→Interestelar). Rango: 0-5.
        /// </summary>
        public decimal CalcularTecnologiaScore(int nivelTecnologico)
        {
            // Normalizar rango 1-4 a 0-5
            decimal score = ((nivelTecnologico - 1) / 3m) * 5m;
            return Math.Round(Math.Min(score, 5m), 2);
        }

        /// <summary>
        /// Score de ubicación estratégica. MVP: valor fijo 2.5.
        /// </summary>
        public decimal CalcularUbicacionScore() => 2.5m;

        /// <summary>
        /// Score de riesgo por amenazas detectadas. 0 sin amenazas, 3 con amenazas.
        /// </summary>
        public decimal CalcularRiesgoScore(bool tieneAmenazas = false) =>
            tieneAmenazas ? 3m : 0m;

        /// <summary>
        /// Valor total = promedio de 5 scores × 100. Rango: 0-500.
        /// </summary>
        public decimal CalcularValorTotal(
            decimal recursosScore,
            decimal poderScore,
            decimal tecnologiaScore,
            decimal ubicacionScore,
            decimal riesgoScore)
        {
            decimal promedio   = (recursosScore + poderScore + tecnologiaScore + ubicacionScore + riesgoScore) / 5m;
            decimal valorTotal = promedio * 100m;
            return Math.Round(valorTotal, 2);
        }

        /// <summary>
        /// Clasifica el planeta: A ≥ 400, B ≥ 300, C ≥ 200, D &lt; 200.
        /// </summary>
        public string CalcularClasePlaneta(decimal valorTotal) =>
            valorTotal switch
            {
                >= 400m => "A",
                >= 300m => "B",
                >= 200m => "C",
                _       => "D"
            };

        /// <summary>
        /// Precio final = valor_total × 1.5.
        /// </summary>
        public decimal CalcularPrecioFinal(decimal valorTotal) =>
            Math.Round(valorTotal * FACTOR_PRECIO_FINAL, 2);
    }
}
