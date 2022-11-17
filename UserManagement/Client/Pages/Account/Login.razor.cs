using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net;
using System.Net.Http.Json;
using UserManagement.Client.Services.Authorization;
using UserManagement.Shared.Models.Account;

namespace UserManagement.Client.Pages.Account
{
    public partial class Login
    {
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [Inject] private HostAuthenticationStateProvider HostAuthenticationStateProvider { get; set; } = default!;

        private LoginModel model = new();
        private bool sendRequest = false;

        private async Task OnValidSubmitAsync(EditContext context)
        {
            sendRequest = true;

            var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/Account/Login", model);
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                Snackbar.Add("Пользователь успешно авторизован", Severity.Success);
                HostAuthenticationStateProvider.RefreshState();
                StateHasChanged();
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
