using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net;
using System.Net.Http.Json;
using UserManagement.Shared.Models.Account;

namespace UserManagement.Client.Pages.Account
{
    public partial class Register
    {
        [Inject] private ISnackbar Snackbar { get; set; } = default!;

        private RegisterModel model = new();
        private bool sendRequest = false;
        private async Task OnValidSubmitAsync(EditContext context)
        {
            sendRequest = true;

            var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/Account/Register", model);
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                Snackbar.Add("Пользователь успешно зарегистрирован", Severity.Success);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Snackbar.Add(await httpResponseMessage.Content.ReadAsStringAsync(), Severity.Error);
            }

            sendRequest = false;
        }
    }
}
