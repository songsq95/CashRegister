namespace CashRegisterTest
{
	using CashRegister;
    using Moq;
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
			Assert.Equal("stub content", printer.PrintContent);
		}

		[Fact]
		public void Should_print_purchase_content_when_run_process_given_stub_purchase_moc()
		{
			// Given
			var spyPrinter = new Mock<Printer>();
			var cashRegister = new CashRegister(spyPrinter.Object);
			var stubPurchase = new Mock<Purchase>();
			stubPurchase.Setup(x => x.AsString()).Returns("moq stub content");
			// when
			cashRegister.Process(stubPurchase.Object);
			// then
			spyPrinter.Verify(_ => _.Print("moq stub content"));
		}

		[Fact]
		public void Should_throw_exception_when_print_is_out_of_paper_use_moc()
		{
			// Given
			var stubPrinter = new Mock<Printer>();
			var cashRegister = new CashRegister(stubPrinter.Object);
			var purchase = new Purchase();
			stubPrinter.Setup(x => x.Print(It.IsAny<string>())).Throws<PrinterOutOfPaperException>();
			// when
			// then
			Assert.Throws<HardwareException>(() => cashRegister.Process(purchase));
		}
	}
}
