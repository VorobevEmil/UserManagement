using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Security.Claims;
using UserManagement.Client.Services.Authorization;
using UserManagement.Client.Shared;
using UserManagement.Shared.Contracts.ManagementUser.Requests;
using UserManagement.Shared.Contracts.ManagementUser.Responses;

namespace UserManagement.Client.Pages
{
    public partial class ManagementUsers
    {
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private HostAuthenticationStateProvider HostAuthenticationStateProvider { get; set; } = default!;
        [CascadingParameter] public MainLayout Parent { get; set; } = default!;

        private HashSet<UserResponse> SelectedUsers = default!;

        private List<UserResponse> Users = default!;
        private string _currentUserId = default!;
        protected override async Task OnInitializedAsync()
        {
            Users = (await HttpClient.GetFromJsonAsync<List<UserResponse>>("api/ManagementUser/GetAll"))!;
            _currentUserId = (await HostAuthenticationStateProvider.GetAuthenticationStateAsync()).User.Claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value;
        }

        private async Task RefreshStatusBlockAsync(bool statusBlock)
        {
            if (SelectedUsers == null || SelectedUsers.Count == default)
            {
                Snackbar.Add("Пользователи не выделены", Severity.Warning);
                return;
            }

            bool? result = await ShowMessageBoxAsync(statusBlock ? "заблокировать" : "разблокировать");

            if (result != true)
                return;

            var userBlockRequest = new UserBlockRequest()
            {
                StatusBlock = statusBlock,
                UsersId = SelectedUsers.Select(t => t.Id).ToList()
            };

            var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/ManagementUser/RefreshStatusBlock", userBlockRequest);
            if (httpResponseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Snackbar.Add(await httpResponseMessage.Content.ReadAsStringAsync(), Severity.Error);
            }
            else
            {
                Snackbar.Add($"Пользователи {(statusBlock ? "заблокированы" : "разблокированы")}!", Severity.Success);
            }

            Users.Where(user => SelectedUsers.Select(t => t.Id).Contains(user.Id)).ToList().ForEach(t => t.StatusBlock = (statusBlock ? "Заблокирован" : "Незаблокирован"));

            if (SelectedUsers.Select(t => t.Id).Contains(_currentUserId))
            {
                await LogoutAsync();
            }
            SelectedUsers.Clear();
        }

        private async Task DeleteUsersAsync()
        {
            if (SelectedUsers == null || SelectedUsers.Count == default)
            {
                Snackbar.Add("Пользователи не выделены", Severity.Warning);
                return;
            }

            bool? result = await ShowMessageBoxAsync("удалить");

            if (result != true)
                return;

            foreach (var userId in SelectedUsers.Select(t => t.Id).ToList())
            {
                var httpResponseMessage = await HttpClient.DeleteAsync($"api/ManagementUser/Delete/{userId}");
                if (httpResponseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Snackbar.Add(await httpResponseMessage.Content.ReadAsStringAsync(), Severity.Error);
                }
            }

            Snackbar.Add("Пользователи удалены!", Severity.Success);
            Users.RemoveAll((user => SelectedUsers.Contains(user)));
            if (SelectedUsers.Select(t => t.Id).Contains(_currentUserId))
            {
                await LogoutAsync();
            }
            SelectedUsers.Clear();
        }

        private async Task<bool?> ShowMessageBoxAsync(string action)
        {
            bool? result = await DialogService.ShowMessageBox(
            "Внимание",
            $"Вы действитель хотите {action} выделенных пользователей?",
            yesText: "Да!", cancelText: "Отмена");

            return result;
        }

        public async Task LogoutAsync()
        {
            await HttpClient.PostAsync("api/Account/Logout", null);
            HostAuthenticationStateProvider.RefreshState();
            await Parent.RefreshStateAsync();
        }
    }
}
