using System.Configuration;


string defaultValue = ConfigurationManager.AppSettings["defaultValue"];


var TicketBooking = new TicketBooking();
TicketBooking.Booking(1);
TicketBooking.Booking(7);
TicketBooking.Booking(12);


public class TicketBooking
{
    public StrategyOfBooking StrategyOfBooking { get; set; }

    string path = @"C:\ZhuoliYu\Lab1DesignPatterns\Lab1DesignPatterns\Records.txt";

    string defaultValue = ConfigurationManager.AppSettings["defaultValue"];


    public double Booking(int month)
    {
        string RecordSaving = "";
        if(month == 12)
        {
            StrategyOfBooking = new DoublePrice();
            RecordSaving = $"{defaultValue} {month} 0 {StrategyOfBooking.Booking()}";
            
        } else if(month == 6 || month == 7)
        {
            StrategyOfBooking= new DiscountPrice();
            RecordSaving = $"{defaultValue} {month} 25 {StrategyOfBooking.Booking()}";
        } else
        {
            StrategyOfBooking = new RegularPrice();
            RecordSaving = $"{defaultValue} {month} 0 {StrategyOfBooking.Booking()}";
        }

            try
            {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(RecordSaving);
            writer.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        
        return StrategyOfBooking.Booking();
    }
}

public interface StrategyOfBooking
{
    public double Booking();
}

public class RegularPrice : StrategyOfBooking
{

    public double Booking()
    {
        return 25;
    }
}

public class DiscountPrice : StrategyOfBooking
{
    string defaultValue = ConfigurationManager.AppSettings["defaultValue"];

    public double Booking()
    {
        return Convert.ToDouble(defaultValue) * 0.75;
    }

}

public class DoublePrice : StrategyOfBooking
{
    string defaultValue = ConfigurationManager.AppSettings["defaultValue"];

    public double Booking()
    {
        return Convert.ToDouble(defaultValue) * 2;
    }
}
