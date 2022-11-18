using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace UserManagement.Client.Shared
{
    public partial class MainLayout
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        private bool _userIsAuthenticated;
        private bool _drawerOpen;

        protected override async Task OnInitializedAsync()
        {
            var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            _userIsAuthenticated = user.Identity!.IsAuthenticated;
            _drawerOpen = _userIsAuthenticated;
            StateHasChanged();
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        public async Task RefreshStateAsync()
        {
            await OnInitializedAsync();
        }
    }
}
