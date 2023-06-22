using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs.Hw0620
{
    public class Button : IClickable
    {
        public void ClickThisObject(bool isClick)
        {
            Console.Write("TEST");
        }
    }
    public interface IClickable
    {
        void ClickThisObject(bool isClick);
    }
}
