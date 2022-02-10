using System;
using System.Collections.Generic;

namespace Answers;

public enum Priority
{
      High = 1
     ,Medium = 2
     ,Low = 3
}

public class PoliceDispatchService
{
    private int[] _teams;
    private Queue<int> _teamQueue;
    private PriorityQueue<string, Priority> _incomingCalls;

    /// <summary>
    /// Creates a new instance of PoliceDispatchService with the given number of teams. Defaults to 4
    /// </summary>
    public PoliceDispatchService(int numberOfTeams = 4)
    {
        _teams = new int[numberOfTeams];
        _teamQueue = new Queue<int>();
        for (int i = 0; i < numberOfTeams; i++)
        {
            _teamQueue.Enqueue(i);
        }
        _incomingCalls = new PriorityQueue<string, Priority>();
    }

    /// <summary>
    /// Takes in a caller name and a priority, adds it to the queue, and checks to see if a team can be dispatched
    /// </summary>
    public List<string> ReceiveCall(string caller, Priority priority)
    {
        var statuses = new List<string>();
        
        _incomingCalls.Enqueue(caller, priority);
        statuses.Add($"\nReceived {priority} priority call from {caller}");
        statuses.AddRange(TryDispatch());

        PrintToConsole(statuses);

        return statuses;
    }    

    /// <summary>
    /// Takes in the team number to mark as returned to the station, and checks to see if any calls can be responded to
    /// </summary>
    public List<string> ReturnTeamToStation(int teamNumber)
    {
        var statuses = new List<string>();
        var actualTeamNumber = teamNumber - 1;
        
        if (_teams[actualTeamNumber] > 0)
        {
            var callPriority = (Priority)_teams[actualTeamNumber];
            _teams[actualTeamNumber] = 0;
            _teamQueue.Enqueue(actualTeamNumber);
            
            statuses.Add($"\nTeam {teamNumber} has returned from a {callPriority} priority call");
            
            statuses.AddRange(TryDispatch());
        }
        else {
            statuses.Add($"\nTeam {teamNumber} is already at the station");
        }
        
        PrintToConsole(statuses);
        
        return statuses;
    }

    /// <summary>
    /// Checks the call queue and available teams to see if any teams can be dispatched for any pending calls
    /// </summary>
    private List<string> TryDispatch()
    {
        var statuses = new List<string>();
        var valid = true;
        
        do
        {
            if (_incomingCalls.Count > 0 && _incomingCalls.TryPeek(out string? caller, out Priority priority))
            {
                if (_teamQueue.Count > 0)
                {
                    if (_teamQueue.Count == 1 && priority != Priority.High)
                    {
                        statuses.Add("There are no high priority calls and only 1 team at the station, so nobody has been dispatched");
                        valid = false;
                    }
                    else
                    {
                        _incomingCalls.Dequeue();
                        var nextTeam = _teamQueue.Dequeue();
                        _teams[nextTeam] = (int)priority;

                        statuses.Add($"Team {nextTeam+1} has been dispatched to respond to a {priority} priority call from {caller}");
                    }
                }
                else
                {
                    statuses.Add("There are currently no teams available to respond to calls");
                    valid = false;
                }
            }
            else
            {
                statuses.Add("There are no more calls to respond to");
                valid = false;
            }
        }
        while (valid);

        return statuses;
    }

    private void PrintToConsole(List<string> strings) =>
        strings.ForEach(s => Console.WriteLine(s));

    /// <summary>
    /// Mostly used for debugging purposes to see the entire state
    /// </summary>
    public List<string> ViewDispatchState()
    {
        var statuses = new List<string>();
        statuses.Add($"\nThere are {_teams.Length} team(s) total. {_teamQueue.Count} team(s) are at the station");
        if (_teamQueue.Count > 0)
        {
            statuses.Add($"Team {_teamQueue.Peek() + 1} is the next available team");
        }

        for (int i = 0; i < _teams.Length; i++)
        {
            if (_teams[i] == 0)
            {
                statuses.Add($"\tTeam {i+1} is waiting at the station");
            }
            else
            {
                statuses.Add($"\tTeam {i+1} is currently responding to a {(Priority)_teams[i]} priority call");
            }
        }
        
        statuses.Add($"There are currently {_incomingCalls.Count} call(s) in the queue");
        if (_incomingCalls.Count > 0 && _incomingCalls.TryPeek(out string? caller, out Priority priority))
        {
            statuses.Add($"\tA {priority} priority call from {caller} is next in the queue");
        }

        PrintToConsole(statuses);

        return statuses;
    }

}