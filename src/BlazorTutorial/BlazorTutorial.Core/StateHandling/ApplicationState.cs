namespace BlazorTutorial.Core.StateHandling
{
    using MudBlazor;

    public class ApplicationState
    {
        public ApplicationState(ISnackbar snackbar)
        {
            GlobalNotification = snackbar;
        }

        public ISnackbar GlobalNotification { get; set; }
    }
}