using System;
//TODO: Убрать ненужные using.
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    /// <summary>
    /// Исключение выдавливания
    /// </summary>
    public class WrapperExtrudeException : Exception
    {
        //TODO: Комментарий.
        public WrapperExtrudeException(string message) : base(message) { }
    }
}
