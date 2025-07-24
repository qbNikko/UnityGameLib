using UnityGameLib.Component;

namespace UnityGameLib.Component.Pool
{
    public class TestPoolObject : SimplePooledObject
    {
        public int state;

        public override void Initialize()
        {
            state = 1;
        }

        public override void Reset()
        {
            state = 2;
        }
        
        public override void Release()
        {
            state = 3;
        }

        public override void Dispose()
        {
            state = 4;
        }
    }
}