using ApiLogic;
using WrapperLib;

namespace InventorWrapperTests
{
    public class Tests
    {
        [Test]
        public void CreatePartTest()
        {
            WrapperFactory factory = new WrapperFactory();
            var wrapper = factory.MakeWrapper(Cad.AutoCad);
            Assert.Throws<WrapperCreatePartException>(() => wrapper.CreatePart());

            wrapper.OpenCad();
            Assert.DoesNotThrow(() => wrapper.CreatePart());
        }

        [Test]
        public void NewRectangleTest()
        {
            WrapperFactory factory = new WrapperFactory();
            var wrapper = factory.MakeWrapper(Cad.AutoCad);
            wrapper.OpenCad();

            Assert.Throws<WrapperNewRectangleException>(() => wrapper.NewRectangle(0, 0, 10, 10, "Some name"));
            Assert.DoesNotThrow(() => wrapper.CreatePart());

            Assert.Throws<WrapperNewRectangleException>(() => wrapper.NewRectangle(0, 0, 0, 0, "Some name"));
            Assert.DoesNotThrow(() => wrapper.NewRectangle(0, 0, 10, 10, "Some name"));

        }

        [Test]
        public void ExtrudeTest()
        {
            WrapperFactory factory = new WrapperFactory();
            var wrapper = factory.MakeWrapper(Cad.AutoCad);
            wrapper.OpenCad();

            Assert.DoesNotThrow(() => wrapper.CreatePart());

            Assert.Throws<WrapperExtrudeException>(() => wrapper.Extrude(10, "Some name", true));

            Assert.DoesNotThrow(() => wrapper.NewRectangle(0, 0, 10, 10, "Some name"));

            Assert.Throws<WrapperExtrudeException>(() => wrapper.Extrude(-10, "Some name", true));

            Assert.DoesNotThrow(() => wrapper.Extrude(10, "Some name", true));
        }
    }
}