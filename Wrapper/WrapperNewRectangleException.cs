using System;
//TODO: Убрать ненужные using.
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    /// <summary>
    /// Исключение создания эскиза с прямоугольником
    /// </summary>
    public class WrapperNewRectangleException : Exception
    {
        //TODO: Комментарий.
        public WrapperNewRectangleException(string message) : base(message) { }
    }
}
