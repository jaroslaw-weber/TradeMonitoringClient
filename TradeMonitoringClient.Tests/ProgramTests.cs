using System;
using System.Threading.Tasks;
using TradeMonitoringClient.Data;
using Xunit;

namespace TradeMonitoringClient.Tests
{
    /// <summary>
    /// Testing Program.cs & Startup.cs
    /// </summary>
    public class ProgramTests
    {
        [Fact]
        public void TestHostBuilder()
        {
            var builder = Program.CreateHostBuilder(new string[]{ });

            Assert.NotNull(builder);
            var host = builder.Build();
            Assert.True(builder.Properties.Count > 0, "no properties");
            Assert.NotNull(host);
        }
    }
}
