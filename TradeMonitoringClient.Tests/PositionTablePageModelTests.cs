using System;
using TradeMonitoringClient.Data;
using Xunit;

namespace TradeMonitoringClient.Tests
{
    public class PositionTablePageModelTests
    {
        [Fact]
        public void TestRecalculate()
        {
            var message = new PositionDataMessage();
            var position = new PositionData();
            position.Id = 1;
            position.DayStartQuantity = 10;
            position.CurrentQuantity = 90;
            message.Positions = new PositionData[] { position };
            message.Timestamp = DateTime.Now;
            var model = new PositionTablePageModel();
            model.OnDataReceived(message);


            //test if data recalculated
            Assert.Equal(80, position.ChangeToday);
            Assert.NotEmpty(model.TimestampString);
            Assert.Equal("green",position.Color);

        }
    }
}
