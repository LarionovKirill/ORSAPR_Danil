using KompasWrapperLib;
using InventorWrapperLib;
using WrapperLib;

namespace ApiLogic
{
    /// <summary>
    /// Фабрика объектов Wrapper
    /// </summary>
    public class WrapperFactory
    {
        /// <summary>
        /// Создать Wrapper в зависимости от выбранной САПР
        /// </summary>
        /// <param name="cad">САПР</param>
        /// <returns>Необходимая реализация Wrapper</returns>
        public IWrapper MakeWrapper(Cad cad)
        {
            switch (cad)
            {
                case Cad.Kompas: return new KompasWrapper();
                case Cad.AutoCad: return new InventorWrapper();
                default: return new KompasWrapper();
            }
        }
    }
}
