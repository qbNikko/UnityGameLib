#if UNITY_INCLUDE_TESTS 
using NUnit.Framework;

namespace UnityGameLib.Component.Pool
{
    
    
    [TestFixture]
    public class PoolTest
    {
        [Test]
        public void TestObjectPool()
        {
            ObjectPoolManager<TestPoolObject> _poolInstance = new(10,1,true,()=>new TestPoolObject());
            TestPoolObject poolObject2 = _poolInstance.Get();
            TestPoolObject poolObject = _poolInstance.Get();
            Assert.AreEqual(2,poolObject.state);
            Assert.AreEqual(2,poolObject2.state);
            poolObject.ReleaseInPool();
            Assert.AreEqual(3,poolObject.state);
            _poolInstance.Get();
            Assert.AreEqual(2,poolObject.state);
            poolObject.ReleaseInPool();
            _poolInstance.Dispose();
            Assert.AreEqual(4,poolObject.state);
            Assert.AreEqual(2,poolObject2.state);
            poolObject2.ReleaseInPool();
            Assert.AreEqual(4,poolObject2.state);
        }
        
    }
}
#endif