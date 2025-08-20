using NUnit.Framework;

namespace UnityGameLib.Component
{
    [TestFixture]
    public class DiServiceTest
    {
        public class Service1
        {
            public string service;

            public Service1(string service)
            {
                this.service = service;
            }
        }
        
        public class Service2
        {
            public string service;

            public Service2(string service)
            {
                this.service = service;
            }
        }
        
        [Test]
        public void TestSingletonService()
        {
            DIService.Instance.RegisterSingleton(new Service2("Service2"));
            using (DIService.Instance.RegisterSingleton(new Service1("Service1")))
            {
                DIService.Instance.GetSingleton<Service1>(out var service1);
                DIService.Instance.GetSingleton<Service2>(out var service2);
                Assert.AreEqual("Service1",service1.service);
                Assert.AreEqual("Service2",service2.service);
            }
            Assert.IsFalse(DIService.Instance.GetSingleton<Service1>(out var service1_out));
            Assert.IsNull(service1_out);
        }
        
        [Test]
        public void TestPrototypeService()
        {
            DIService.Instance.RegisterMulti(new Service1("Service2"));
            using (DIService.Instance.RegisterMulti(new Service1("Service1")))
            {
                Assert.IsTrue(DIService.Instance.GetAll<Service1>(out var services));
                Assert.AreEqual(2,services.Count);
                Assert.AreEqual("Service2",services[0].service);
                Assert.AreEqual("Service1",services[1].service);
            }
            Assert.IsTrue(DIService.Instance.GetAll<Service1>(out var services2));
            Assert.AreEqual(1,services2.Count);
            Assert.AreEqual("Service2",services2[0].service);
        }
    }
}