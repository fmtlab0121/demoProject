using MultipleProject.Models;
using System;
using Xunit;

namespace DemoLab_Scrum.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void AddingNewCustomerTest()
        {
            Customer customer = new Customer(1, "Bernard");
            Assert.Equal(1, customer.Id);
            Assert.Equal("Bernard", customer.Name);
        }
    }
}
