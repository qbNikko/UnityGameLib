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
            TestPoolObject poolObject = _poolInstance.Get();
            Assert.AreEqual(2,poolObject.state);
            poolObject.ReleaseInPool();
            Assert.AreEqual(3,poolObject.state);
            _poolInstance.Get();
            Assert.AreEqual(2,poolObject.state);
            poolObject.ReleaseInPool();
            _poolInstance.Dispose();
            Assert.AreEqual(4,poolObject.state);
        }
        
    }
}