using System.Runtime.CompilerServices;

namespace WMS.Core.Infrastructure
{
    public class EngineContext
    {
        #region Methods

        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
           
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new WMSEngine());
        }

        
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        #region Properties

        
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance;
            }
        }

        #endregion

    }
}
