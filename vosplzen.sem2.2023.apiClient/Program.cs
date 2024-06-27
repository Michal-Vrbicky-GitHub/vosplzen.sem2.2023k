
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using vosplzen.sem2._2023.apiClient;
using vosplzen.sem2._2023.apiClient.Contracts;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        string connectionString = config["Sql:ConnectionString"];

        string apiBaseUri = config["Api:BaseUri"];
        string token = config["Api:AuthToken"];

        string logFolder = config["App:LogFolder"];
        string errorFolder = config["App:ErrorFolder"];

        // 
        while (true)
        {
            await SendValues(config);
            await Task.Delay(TimeSpan.FromSeconds(5)); // Počkejte 5 sekund před dalším voláním
        }
    }



    public static async Task SendValues(IConfiguration config)
    {
        string apiBaseUri = config["Api:BaseUri"];
        string token = config["Api:AuthToken"];

        var message = GenerateFloodReport();

        using (var apiclient = new ApiClient())
        {
            await apiclient.SendMessage(message, apiBaseUri, token);
        }
    }



    public static FloodReport GenerateFloodReport()
    {
        Random random = new Random();
        int value = random.Next(1, 101);
        int stationId = random.Next(1, 11);
        DateTime now = DateTime.Now;
        DateTime oneYearAgo = now.AddYears(-1);
        /*
         * Unhandled exception. System.ArgumentOutOfRangeException: Year, Month, and Day parameters describe an un-representable DateTime.
   at System.DateTime..ctor(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second)
   at Program.GenerateFloodReport() in D:\dlouhyyy\SORS\reportSender a tak\dyť ho mam přímo před čumákem tvl\vosplzen.sem2.2023k-master\vosplzen.sem2.2023.apiClient\Program.cs:line 59
   at Program.SendValues(IConfiguration config) in D:\dlouhyyy\SORS\reportSender a tak\dyť ho mam přímo před čumákem tvl\vosplzen.sem2.2023k-master\vosplzen.sem2.2023.apiClient\Program.cs:line 41
   at Program.Main(String[] args) in D:\dlouhyyy\SORS\reportSender a tak\dyť ho mam přímo před čumákem tvl\vosplzen.sem2.2023k-master\vosplzen.sem2.2023.apiClient\Program.cs:line 29
   at Program.<Main>(String[] args)

D:\dlouhyyy\SORS\reportSender a tak\dyť ho mam přímo před čumákem tvl\vosplzen.sem2.2023k-master\vosplzen.sem2.2023.apiClient\bin\Debug\net7.0\vosplzen.sem2.2023.apiClient.exe (proces 18200) byl ukončen s kódem 0.
*/        //DateTime dateTime = new DateTime(now.Year, random.Next(1, 13), random.Next(1, 32), random.Next(0, 24), random.Next(0, 60), random.Next(0, 60));
        int monde = random.Next(1, 13);
        DateTime dateTime = new DateTime(
	    	now.Year,
	    	monde,
	    	//random.Next(1, DateTime.DaysInMonth(now.Year, now.Month) + 1),
	     	random.Next(1, DateTime.DaysInMonth(now.Year, monde) + 1),
	    	random.Next(0, 24),    // 
	    	random.Next(0, 60),    
	    	random.Next(0, 60)
		);
		if (dateTime > now)
        {
            dateTime = now;
        }
        else if (dateTime < oneYearAgo)
        {
            dateTime = oneYearAgo;
        }

        FloodReport floodReport = new FloodReport
        {
            StationId = stationId,
            TimeStamp = dateTime,																				//Závažnost	Kód	Popis	Projekt	Soubor	Řádek	Stav potlačení Chyba	CS0117	FloodReport neobsahuje definici pro DateTime.	vosplzen.sem2.2023.apiClient	D:\dlouhyyynaaaaazvyyyy\vosplzen.sem2.2023k-master\vosplzen.sem2.2023.apiClient\Program.cs	73	Aktivní
            Value = value,
			Token = "ChangeToRealToken",
	};

        return floodReport;
    }

}