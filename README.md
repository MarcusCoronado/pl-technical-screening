# Technical Screening - Marcus Coronado

Overall Assumptions/Notes:
- Assumed valid values will be passed in. Guard clauses and proper error handling are ideal in actual code
- Output is written to the Console
- We'll have to make do with manually editing the console's Program.cs or the unit tests for additional scenarios/testing

<br>

## Answer 1 - Loan Estimation

Assumptions:
- Small rounding errors are fine

Thoughts for improvement:
- The amortization schedule seems to have some rounding errors compared to the online sources I was comparing against. As a result the total interest is also slightly off
- The Console output only pretty prints nicely up to certain lengths. More robust, dynamic handling of that output would be nice
- Add in options for other payment periods such as yearly, quarterly, etc

Sources:
- Formula for calculating monthly payment: https://www.kasasa.com/blog/how-to-calculate-loan-payments-in-3-easy-steps
- Schedule references: https://www.bankrate.com/calculators/mortgages/loan-calculator.aspx, https://www.calculator.net/amortization-calculator.html

<br>

## Answer 2 - Word Construction

Assumptions:
- All inputs are lower case

Notes:
- Fairly straightforward frequency array. Using the assumption of all lowercase let's us take advantage of just using `(character - 'a')` to get the proper character's index within the array

<br>

## Answer 4 - Police Station Dispatch Service

Assumptions:
- Toy implementation takes place entirely in memory

Notes:
- There are two main methods for the service
  - ReceiveCall which takes a caller's name, and priority
  - ReturnTeamToStation which marks a team as available to respond to calls

<br>

## Answer 6 - Current Open Tickets SQL Report

Assumptions:
- EndDate is the only input parameter

Instructions:
- Run CurrentOpenTickets.sql against the database with the given tables, or uncomment the table creations and insertions and run it all against a new database
- Add more Tickets and StatusChanges if desired
- Call `EXEC [dbo].[CurrentOpenTickets] N'yyyy-MM-ddTHH:mm:ss'`
  - Some examples I used to test with are at the bottom of the sql script

Thoughts for improvement:  
- We can show only relevant tickets by removing the `LEFT JOIN [statuses_cte]` to `INNER JOIN`
- Most reports I've written usually take in a start date as well as end date. We can account for the start date by:
  - Adding `@StartDate DateTime2 = NULL` to the parameters along with a sensible default (say the beginning of the current year or quarter)
  - Adding a check to make sure that EndDate is greater than StartDate
  - Replacing `sOld.[Timestamp]` on line 49 with `CASE WHEN @startDate > sOld.[Timestamp] THEN @startDate ELSE sOld.[Timestamp] END`
  - Add `AND (sNew.[Timestamp] IS NULL OR sNew.[Timestamp] >= @startDate)` to the WHERE clause in the CTE

<br>

## Dev Notes and Running

I recently built a new desktop and it doesn't have Visual Studio on it yet. I've been wanting to play around with developing smaller solutions in **VS Code and .Net 6**, so I took the opportunity. Please let me know if this is an issue. All commands below can be used in the VS Code terminal, or a terminal of your choice.

To build: `dotnet build`  
To run the console app: `dotnet run --project Console`  
To run tests: `dotnet test AnswersTests/AnswersTests.csproj`