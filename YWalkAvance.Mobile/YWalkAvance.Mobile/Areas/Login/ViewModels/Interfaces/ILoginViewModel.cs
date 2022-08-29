using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Mobile.Areas.Login.ViewModels.Interfaces
{
    public interface ILoginViewModel
    {
        ICommand DoLoginCommand { get; set; }
        ICommand CheckLoginCommand { get; set; }
        ICommand DoShowPassword { get; set; }
        // ASOSA IsSessionClosed ORIGINAL
        //bool IsSessionClosed();
        Task<bool> IsSessionClosed();
        Task<bool> IsSyncFinished();

        Task<string> GetUser();
        //Task DeleteUserData();
        //bool IsUserInputEnabled { get; set; }
        //void DoDeleteUser();
        void SetInput(Entry input);
        //void SetBtnDeleteUser(Button btnDeleteUser);
        void SetPassInput(Entry passEntry);
        string GetVersionCode();
    }
}
