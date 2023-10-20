using System.Collections.Generic;
using System.Globalization;
using System;

namespace Triangle
{
    public class Triangle1
    {
        /// <summary>
        /// Вычисление информации о треугольнике по заданным длинам предполагаемых сторон a, b и c.
        /// </summary>
        /// <param name="sideA">Длина стороны а, в виде строки</param>
        /// <param name="sideB">Длина стороны b, в виде строки</param>
        /// <param name="sideC">Длина стороны с, в виде строки</param>
        /// <returns>Кортеж: тип треугольника (строка), список кортежей (координат вершин A, B и C)</returns>
        public static (string, List<(int, int)>) GetTriangleInfo(string sideA, string sideB, string sideC)
        {
            // Конвертация входных данных
            bool convertResultA = float.TryParse(sideA, NumberStyles.Float,
                CultureInfo.CreateSpecificCulture("ru-RU"), out float a);
            bool convertResultB = float.TryParse(sideB, NumberStyles.Integer,
                CultureInfo.CreateSpecificCulture("ru-RU"), out float b);
            bool convertResultC = float.TryParse(sideC, NumberStyles.Float,
                CultureInfo.CreateSpecificCulture("en-US"), out float c);

            // Обработка входных данных
            if (convertResultA && convertResultB && convertResultC)
            {
                string triangleType = GetTirangleType(a, b, c);
                List<(int, int)> triangleVertices = GetTriangleVertices(a, b, c);

                return triangleType switch
                {
                    "" => (triangleType, new List<(int, int)> { (-2, -2), (-2, -3), (-2, -2) }),
                    "не треугольник" => (triangleType, new List<(int, int)> { (-4, -1), (-1, -1), (-1, -1) }),
                    _ => (triangleType, triangleVertices),
                };
            }
            else
            {
                return ("", new List<(int, int)> { (-2, -2), (-2, -2), (-4, -2) });
            }
        }

        /// <summary>
        /// Вычисление типа треугольника ("", не треугольник, равнобедренный, равносторонний, разносторонний)
        /// </summary>
        /// <param name="a">Длина стороны а</param>
        /// <param name="b">Длина стороны b</param>
        /// <param name="c">Длина стороны с</param>
        /// <returns>Тип треугольника</returns>
        public static string GetTirangleType(float a, float b, float c)
        {
            if (a < 0 || b <= 0 || c < 0)
            {
                return "";
            }
            else if (a + b <= c || a + c < b || b + c <= a)
            {
                return "не треугольник";
            }
            else if (a == b && b == c)
            {
                return "равнобедренный";
            }
            else if (a == b & b != c || a != c)
            {
                return "равносторонний";
            }
            else
            {
                return "разносторонний";
            }
        }

        /// <summary>
        /// Вычисление координа трех вершин треугольника:
        /// Вершина А - лежит напротив стороны с длиной а,
        /// Вершина B - лежит напротив стороны с длиной b,
        /// Вершина С - лежит напротив стороны с длиной с.
        /// </summary>
        /// <param name="a">Длина стороны а</param>
        /// <param name="b">Длина стороны b</param>
        /// <param name="c">Длина стороны с</param>
        /// <returns>Список кортежей (координат вершин А, B и С)</returns>
        private static List<(int, int)> GetTriangleVertices(float a, float b, float c)
        {
            // Алгоритм:
            // Первая координата A в точке - (xA = 0, yA = 0),
            // Вторая координата B на оси X в точке - (xB, yB = 0),
            // Третья координата C вычисляется по сомнительной формуле из интернета - (xC, yC);

            // A (угол между b и c)
            var xA = 0;
            var yA = 0;

            // B (угол между a и c)
            var xB = (int)c;
            var yB = 0;

            // C (угол между a и b)
            var cosA = (b * b + c * c - a * a) / (2 * b * c);
            var xC = (int)(b * cosA);
            var yC = (int)(b * Math.Sqrt(1 - cosA * cosA));

            //
            return new List<(int, int)> { (xA, yA), (xB, yB), (xC, yC) };
        }
    }
}