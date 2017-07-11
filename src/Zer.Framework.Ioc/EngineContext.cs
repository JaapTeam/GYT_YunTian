namespace Zer.Framework.Ioc
{
    public class EngineContext
    {
        private static EngineContext current = null;
        private static object syncObj = new object();

        public static EngineContext Current
        {
            get
            {
                if (current == null)
                {
                    lock (syncObj)
                    {
                        if (current == null)
                        {
                            current = new EngineContext();
                        }
                    }
                }

                return current;
            }
        }
        /// <summary>
        /// The Singleton instance.
        /// </summary>
        //public static EngineContext Instance { get; set; }

        public T Resolve<T>() where T : class
        {
            var resolver = DiConfig.Resolve<T>();

            return resolver;
        }
    }
}