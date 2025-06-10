using System;
using System.Windows.Controls;

namespace PilotApp.Services
{
    public class NavigationService
    {
        private ContentPresenter contentPresenter;

        public ContentPresenter ContentPresenter
        {
            get
            {
                return this.contentPresenter;
            }

            set
            {
                this.contentPresenter = value;
            }
        }

        public void Initialize(ContentPresenter contentPresenter)
        {
            this.ContentPresenter = contentPresenter;
        }

        public void NaviguerVers(UserControl view)
        {
            if (this.ContentPresenter != null)
            {
                ContentPresenter.Content = view;
            }
        }

        public void NaviguerVers<T>() where T : UserControl, new()
        {
            var view = new T();
            NaviguerVers(view);
        }
    }
}