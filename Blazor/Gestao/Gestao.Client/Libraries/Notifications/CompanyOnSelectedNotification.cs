namespace Gestao.Client.Libraries.Notifications
{
    public class CompanyOnSelectedNotification
    {
        public Action? OnCompanySelected { get; set; }
        public void NotificationOnSelected()
        {
            OnCompanySelected?.Invoke();
        }
    }
}
