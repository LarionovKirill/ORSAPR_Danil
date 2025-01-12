using ParametersLogic;
using WrapperLib;

namespace ApiLogic
{
    /// <summary>
    /// Построитель модели стола
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Ссылка на объект работы с API САПР
        /// </summary>
        private IWrapper _wrapper;

        /// <summary>
        /// Параметры построения модели
        /// </summary>
        private Parameters _parameters;

        /// <summary>
        /// Счетчик созданных проектов в САПР
        /// </summary>
        private static int counter = 0;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="parameters">Параметры модели стола</param>
        /// <param name="cad">САПР, в которой будет происходить построение</param>
        public Builder(Parameters parameters, Cad cad)
        {
            // TODO: Что это за комментарий к методу?
            WrapperFactory factroy = new WrapperFactory();
            _wrapper = factroy.MakeWrapper(cad);
            _parameters = parameters;
        }

        /// <summary>
        /// Построить стол
        /// </summary>
        public void Build()
        {
            if (!_wrapper.IsCadRunning() || counter == 23)
            {
                _wrapper.OpenCad();
            }
            _wrapper.CreatePart();
            counter++;

            var topWidth = _parameters.Params[ParamType.TopWidth].Value;
            var topDepth = _parameters.Params[ParamType.TopDepth].Value;
            var topHeight = _parameters.Params[ParamType.TopHeight].Value;
            var legWidth = _parameters.Params[ParamType.LegWidth].Value;
            var tableHeight = _parameters.Params[ParamType.TableHeight].Value;
            int legHeight = tableHeight - topHeight;
            double originX = 0;
            double originY = 0;

            string topName = "Top";
            _wrapper.NewRectangle(originX, originY, topWidth, topDepth, topName);
            _wrapper.Extrude(topHeight, topName, true);

            string[] legsNames = { "Leg1", "Leg2", "Leg3", "Leg4" };
            _wrapper.NewRectangle(originX,
                                  originY,
                                  legWidth,
                                  legWidth,
                                  legsNames[0]);
            _wrapper.Extrude(legHeight, legsNames[0], false);

            _wrapper.NewRectangle(topWidth - legWidth,
                                  originY,
                                  legWidth,
                                  legWidth,
                                  legsNames[1]);
            _wrapper.Extrude(legHeight, legsNames[1], false);

            _wrapper.NewRectangle(originX,
                                  topDepth - legWidth,
                                  legWidth,
                                  legWidth,
                                  legsNames[2]);
            _wrapper.Extrude(legHeight, legsNames[2], false);

            _wrapper.NewRectangle(topWidth - legWidth,
                                  topDepth - legWidth,
                                  legWidth,
                                  legWidth,
                                  legsNames[3]);
            _wrapper.Extrude(legHeight, legsNames[3], false);
        }
    }
}
