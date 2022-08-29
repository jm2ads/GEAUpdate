using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Observers.Interfaces
{
    public interface IObserver
    {
        void OnNotify(string message);
    }
}
