using System;
//TODO: Убрать ненужные using.
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLib
{
    /// <summary>
    /// Исключение создания детали
    /// </summary>
    public class WrapperCreatePartException : Exception
    {
        //TODO: Комментарий.
        public WrapperCreatePartException(string message) : base(message) { }
    }
}
