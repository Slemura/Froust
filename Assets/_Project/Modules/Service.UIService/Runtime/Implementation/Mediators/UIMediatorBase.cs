using System;

namespace RpDev.Services.UI.Mediators
{
    public class UIMediatorBase<T> where T : UIScreen
    {
        protected T View { get; private set; }

        public void RegisterView(T view)
        {
            View = view;
        }
    }
}