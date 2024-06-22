namespace StateManagement.States
{
    public class StateContainer
    {
        private int _counter;
        public int Counter { 
            get { return _counter; }
            set { 
                _counter = value;
                NotificationHasChanged();
            }
        }
        public Action? Notification; //Componentes devem se inscrever nessa Action

        private void NotificationHasChanged()
        {
            Notification?.Invoke();
        }
    }
}
