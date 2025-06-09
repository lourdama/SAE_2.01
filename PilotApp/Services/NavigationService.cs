using System;
using System.Windows.Controls;

namespace PilotApp.Services
{
    public class NavigationService
    {
        private ContentPresenter _contentPresenter;

        public void Initialize(ContentPresenter contentPresenter)
        {
            _contentPresenter = contentPresenter;
        }

        public void NavigateTo(UserControl view)
        {
            if (_contentPresenter != null)
            {
                _contentPresenter.Content = view;
            }
        }

        public void NavigateTo<T>() where T : UserControl, new()
        {
            var view = new T();
            NavigateTo(view);
        }
    }
}