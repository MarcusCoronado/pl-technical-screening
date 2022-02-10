using Answers;
using System;


var hr = "===================================================\n";

LoanEstimation.GetLoanEstimation(200000m, 30, .03m);
Console.WriteLine(hr);
LoanEstimation.GetLoanEstimation(200000m, 20, .025m);

Console.WriteLine(hr);

var constructedWords = WordConstruction.FindWordConstructions(
    new string [] { "good", "bad", "dog", "cat", "do", "dont" }
    ,"ddelgoo"
);

Console.WriteLine(hr);

var policeDispatchService = new PoliceDispatchService();
policeDispatchService.ReceiveCall("Alice", Priority.High);
policeDispatchService.ReceiveCall("Billy", Priority.Low);
policeDispatchService.ReceiveCall("Casey", Priority.Medium);
policeDispatchService.ReceiveCall("Devin", Priority.High);
policeDispatchService.ReceiveCall("Erika", Priority.Low);
policeDispatchService.ReturnTeamToStation(2);
policeDispatchService.ReceiveCall("Frank", Priority.Medium);
policeDispatchService.ReceiveCall("Glenn", Priority.High);
policeDispatchService.ReturnTeamToStation(2);
policeDispatchService.ReturnTeamToStation(3);
policeDispatchService.ReturnTeamToStation(4);
policeDispatchService.ReceiveCall("Haley", Priority.Low);
policeDispatchService.ReturnTeamToStation(3);