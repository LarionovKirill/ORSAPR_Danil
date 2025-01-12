using System.Collections.Generic;

namespace ParametersLogic
{
    /// <summary>
    /// Набор параметров, реализующий валидацию их значений
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Словарь параметров
        /// </summary>
        public Dictionary<ParamType, Parameter> Params { get; set; }

        /// <summary>
        /// Установить параметры и провалидировать их значения
        /// </summary>
        /// <param name="parameters">Словарь параметров</param>
        /// <returns>Список параметров, непрошедших валидацию</returns>
        public List<IncorrectParameters> SetParameters(Dictionary<ParamType, Parameter> parameters)
        {
            var incorrect = Validate(parameters);
            Params = parameters;
            return incorrect;
        }

        /// <summary>
        /// Получить параметры
        /// </summary>
        /// <returns>Словарь параметров</returns>
        public Dictionary<ParamType, Parameter> GetParameters()
        {
            return Params;
        }

        /// <summary>
        /// Провести валидацию переданных параметров
        /// </summary>
        /// <param name="parameters">Словарь параметров</param>
        /// <returns>Список параметров, непрошедших валидацию.
        /// Пустой список если все параметры корректны</returns>
        private List<IncorrectParameters> Validate(Dictionary<ParamType, Parameter> parameters)
        {
            var incorrect = new List<IncorrectParameters>();

            var legWidth = parameters[ParamType.LegWidth];
            var topWidth = parameters[ParamType.TopWidth];
            var topDepth = parameters[ParamType.TopDepth];

            int twoLegsWidth = legWidth.Value * 2 + 200;
            bool wereIncorrect = false;

            foreach (var parameter in parameters)
            {
                var value = parameter.Value;
                switch (parameter.Key)
                {
                    case (ParamType.TopWidth):
                        {
                        // TODO: Мне не нравится, что числа так заданы. Может записать все
                        // числа в константы статические и использовать.
                            value.MinValue = 500;
                            value.MaxValue = 5000;
                            AddIfIncorrect(
                                incorrect, 
                                IncorrectParameters.TopWidthIncorrect, 
                                value);
                            break;
                        }
                    case (ParamType.TopDepth):
                        {
                            value.MinValue = 500;
                            value.MaxValue = 5000;
                            AddIfIncorrect(
                                incorrect, 
                                IncorrectParameters.TopDepthIncorrect, 
                                value);
                            break;
                        }
                    case (ParamType.LegWidth):
                        {
                            value.MinValue = 20;
                            value.MaxValue = 200;
                            AddIfIncorrect(
                                incorrect, 
                                IncorrectParameters.LegWidthIncorrect, 
                                value);
                            break;
                        }
                    case (ParamType.TableHeight):
                        {
                            value.MinValue = 500;
                            value.MaxValue = 1400;
                            AddIfIncorrect(
                                incorrect, 
                                IncorrectParameters.TableHeightIncorrect, 
                                value);
                            break;
                        }
                    case (ParamType.TopHeight):
                        {
                            value.MinValue = 16;
                            value.MaxValue = 100;
                            AddIfIncorrect(
                                incorrect, 
                                IncorrectParameters.TopHeightIncorrect, 
                                value);
                            break;
                        }
                }
            }
            if ((topWidth.Value < twoLegsWidth || topDepth.Value < twoLegsWidth) 
                && !wereIncorrect)
            {
                incorrect.Add(IncorrectParameters.TopAndLegsAreaIncorrect);
                wereIncorrect = true;
            }
            return incorrect;
        }

        /// <summary>
        /// Проверить значение параметра и добавить его к списку некорректных параметров в случае неудачи.
        /// </summary>
        /// <param name="incorrect">Список параметров, непрошедших валидацию</param>
        /// <param name="type">Тип параметра</param>
        /// <param name="value">Параметр</param>
        private void AddIfIncorrect(
            List<IncorrectParameters> incorrect, 
            IncorrectParameters type, 
            Parameter value)
        {
            //TODO: переменная type не используется.
            if (value.Value < value.MinValue || value.Value > value.MaxValue)
            {
                incorrect.Add(IncorrectParameters.TopWidthIncorrect);
            }
        }
    }
}
