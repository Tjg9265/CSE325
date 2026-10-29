# CSE 325 – Week 01 Assignment Notes

## 1. Pizza API Project

I created an ASP.NET Core Web API using .NET 8 and C#.

The project includes a Pizza model, a PizzaService class, and a PizzasController that supports CRUD operations.

### Seeded Pizza Data

I added an additional pizza, BBQ Chicken, to the initial list in PizzaService.cs.

| ID | Name | IsGlutenFree |
|---|---|---|
| 1 | Classic Italian | false |
| 2 | Veggie | true |
| 3 | BBQ Chicken | false |

### HTTP Request Testing

**GET – Retrieve All Pizzas**

Request: `GET http://localhost:5283/pizzas`

Response:
```json
[
  {"id":1,"name":"Classic Italian","isGlutenFree":false},
  {"id":2,"name":"Veggie","isGlutenFree":true},
  {"id":3,"name":"BBQ Chicken","isGlutenFree":false}
]
```

The API successfully returned all three seeded pizzas. The HTTP status code was not separately captured.

**POST – Create a Pizza**

Request: `POST http://localhost:5283/pizzas`

Request body:
```json
{"name":"Pepperoni","isGlutenFree":false}
```

Response status: **201 Created**

Response body:
```json
{"id":4,"name":"Pepperoni","isGlutenFree":false}
```

**PUT – Update a Pizza**

Request: `PUT http://localhost:5283/pizzas/4`

Request body:
```json
{"id":4,"name":"Supreme","isGlutenFree":false}
```

Response status: **204 No Content**

Response body: Empty.

**DELETE – Remove a Pizza**

Request: `DELETE http://localhost:5283/pizzas/4`

Response status: **204 No Content**

Response body: Empty.

**GET – Verify Deletion**

Request: `GET http://localhost:5283/pizzas/4`

Response status: **404 Not Found**

This confirmed that the deleted pizza was no longer available.

## 2. Sales Summary Project

I created a .NET 8 console application that reads sales data from CSV files, calculates individual file totals, and generates a sales-summary.txt file.

### Sales Results

| File | Total Sales |
|---|---:|
| january.csv | $405.75 |
| february.csv | $525.75 |
| march.csv | $401.25 |
| **Grand Total** | **$1,332.75** |

### Generated Text File

```text
SALES SUMMARY
-------------
february.csv: $525.75
january.csv: $405.75
march.csv: $401.25
-------------
TOTAL SALES: $1332.75
```

### Sales Summary Function

The following C# function reads the CSV files, calculates the sales totals, and writes the results to a text file.

```csharp
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
```

## 3. Conclusion

Through this assignment, I practiced building applications with C# and .NET 8. I learned how to create a Web API using controllers, implement CRUD operations, test HTTP requests and status codes, read data from CSV files, and generate a sales summary text file.