using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MobileService.Tests.MockData
{
    [Collection("testdb_impact")]
    public class Initializator
    {
        [Fact]
        public void MockV1()
        {
            var mocker = new MockDataV1();
            mocker.Reset();
        }

        [Fact]
        public void MockV2()
        {
            var mocker = new MockDataV2();
            mocker.Reset();
        }

        [Fact]
        public void MockV3()
        {
            var mocker = new MockDataV3();
            mocker.Reset();
        }

        [Fact]
        public void MockV4()
        {
            var mocker = new MockDataV4();
            mocker.Reset();
        }

        [Fact]
        public void MockV5()
        {
            var mocker = new MockDataV5();
            mocker.Reset();
        }

        [Fact]
        public void MockV6()
        {
            var mocker = new MockDataV6();
            mocker.Reset();
        }
    }
}
