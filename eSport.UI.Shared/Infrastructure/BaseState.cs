using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace eSport.UI.Shared.Infrastructure
{
    public abstract class BaseState<T> : INotifyPropertyChanged
    {
        private T _state;
        public BaseState(T initialState)
        {
            _state = initialState;
        }
        protected void Update(T state)
        {
            SetProperty(ref _state, state);
        }
        protected T State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public event Action? OnChange;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        internal bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string
                                                     propertyName = null)
        {
            if (Equals(storage, value)) return false;


            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
