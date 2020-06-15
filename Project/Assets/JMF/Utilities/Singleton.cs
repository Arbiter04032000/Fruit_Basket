using System.Collections;
using System.Collections.Generic;

namespace JMF.Systems.Utilities
{
    public class Singleton
    {
        //--SINGLETON--
        private static readonly Singleton instance = new Singleton();
        public virtual Singleton Instance { get { return instance; } }

        public Singleton()
        {

        }
    }
}
