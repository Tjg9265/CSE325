
using System.Globalization;
using System.Text;

string salesFolder = Path.Combine(
    Directory.GetCurrentDirectory(), "sales");

string summaryFile = Path.Combine(
    Directory.GetCurrentDirectory(), "sales-summary.txt");

GenerateSalesSummary(salesFolder, summaryFile);

static void GenerateSalesSummary(
    string folderPath, string outputPath)
{
    string[] files = Directory.GetFiles(folderPath, "*.csv");

    decimal grandTotal = 0;
    StringBuilder summary = new StringBuilder();

    summary.AppendLine("SALES SUMMARY");
    summary.AppendLine("-------------");

    foreach (string file in files.OrderBy(f => f))
    {
        decimal fileTotal = 0;

        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] columns = line.Split(',');

            if (columns.Length < 2)
                continue;

            if (decimal.TryParse(
                columns[1],
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal amount))
            {
                fileTotal += amount;
            }
        }

        grandTotal += fileTotal;

        summary.AppendLine(
            $"{Path.GetFileName(file)}: ${fileTotal:F2}");
    }

    summary.AppendLine("-------------");
    summary.AppendLine($"TOTAL SALES: ${grandTotal:F2}");

    File.WriteAllText(outputPath, summary.ToString());

    Console.WriteLine("Sales summary created!");
    Console.WriteLine(summary.ToString());
}
