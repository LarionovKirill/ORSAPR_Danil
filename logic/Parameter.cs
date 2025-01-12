using System.CodeDom;

namespace ParametersLogic
{
    /// <summary>
    /// Параметр, используемый для передачи размеров при построении модели в САПР
    /// </summary>
    public class Parameter
    {
        // TODO: Комментарий и модификтор доступа.
        // Либо использовать как автосвойство, которое у тебя есть уже.
        int _value;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">Числовое значение параметра</param>
        public Parameter(int value, int minValue, int maxValue)
        {
            //TODO: Что за комментарий?.
            _value = value;
            if (value < minValue || value > maxValue)
            {
                throw new System.Exception();
            }
        }

        /// <summary>
        /// Максимальное значение параметра.
        /// </summary>
        public int MaxValue { get; }

        /// <summary>
        /// Минимальное значение параметра.
        /// </summary>
        public int MinValue { get; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public int Value 
        { 
            get => _value;
        }
    }
}
