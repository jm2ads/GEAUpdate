using Business.Observers.Interfaces;
using Commons.Commons.Entities;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> DoLogin(DeviceInfoModel deviceInfo);

        void AddObserver(IObserver obs);
        void RemoveObserver(IObserver obs);
        void Trigger(string triggerMessage);
    }
}
