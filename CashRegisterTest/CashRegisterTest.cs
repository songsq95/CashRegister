namespace CashRegisterTest
{
	using CashRegister;
	using Xunit;

	public class CashRegisterTest
	{
		[Fact]
		public void Should_process_execute_printing()
		{
			//given
			SpyPrinter printer = new SpyPrinter();
			var cashRegister = new CashRegister(printer);
			var purchase = new Purchase();
			//when
			cashRegister.Process(purchase);
			//then
			//verify that cashRegister.process will trigger print
			Assert.True(printer.HasPrinted);
		}

		[Fact]
		public void Should_print_purches_content_when_run_process_given_stub_purchase()
		{
			//given
			SpyPrinter printer = new SpyPrinter();
			var cashRegister = new CashRegister(printer);
			var purchase = new StubPurchase();
			//when
			cashRegister.Process(purchase);
			//then
			//verify that cashRegister.process will trigger print
			Assert.Equal("stub content", printer.PrintContent);
		}
	}
}

