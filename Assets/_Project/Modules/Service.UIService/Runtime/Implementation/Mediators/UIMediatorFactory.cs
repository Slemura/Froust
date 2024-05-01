using RpDev.Services.GenericFactories.VContainer;

namespace RpDev.Services.UI.Mediators
{
    public class UIMediatorFactory
    {
        private readonly IPlainClassFactory _plainClassFactory;

        public UIMediatorFactory(IPlainClassFactory plainClassFactory)
        {
            _plainClassFactory = plainClassFactory;
        }
        
        public TMediator Create<TMediator, TView>(TView view) where TMediator : UIMediatorBase<TView> where TView : UIScreen
        {
            var mediatorInstance = _plainClassFactory.Create<TMediator>();
            
            mediatorInstance.RegisterView(view);
            
            return mediatorInstance;
        }
    }
}