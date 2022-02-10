using Answers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AnswersTests;

[TestClass]
public class PoliceDispatchServiceTests
{
    [TestMethod()]
    public void ReceiveCall_NoQueuedCalls_FullTeam()
    {
        var expected = new List<string> {
            "\nReceived Low priority call from Alice"
            ,"Team 1 has been dispatched to respond to a Low priority call from Alice"
            ,"There are no more calls to respond to"
        };
        var pds = new PoliceDispatchService();

        var result = pds.ReceiveCall("Alice", Priority.Low);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }

    [TestMethod()]
    public void ReceiveCall_NoQueuedCalls_OneTeam_NoHighPriority()
    {
        var expected = new List<string> {
            "\nReceived Medium priority call from Devin"
            ,"There are no high priority calls and only 1 team at the station, so nobody has been dispatched"
        };
        var pds = new PoliceDispatchService();
        pds.ReceiveCall("Alice", Priority.Medium);
        pds.ReceiveCall("Billy", Priority.High);
        pds.ReceiveCall("Casey", Priority.Low);

        var result = pds.ReceiveCall("Devin", Priority.Medium);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }

    [TestMethod()]
    public void ReceiveCall_NoQueuedCalls_OneTeam_HighPriority()
    {
        var expected = new List<string> {
            "\nReceived High priority call from Devin"
            ,"Team 4 has been dispatched to respond to a High priority call from Devin"
            ,"There are no more calls to respond to"
        };
        var pds = new PoliceDispatchService();
        pds.ReceiveCall("Alice", Priority.Low);
        pds.ReceiveCall("Billy", Priority.Low);
        pds.ReceiveCall("Casey", Priority.Low);

        var result = pds.ReceiveCall("Devin", Priority.High);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }

    [TestMethod()]
    public void ReceiveCall_NoQueuedCalls_EmptyTeam()
    {
        var expected = new List<string> {
            "\nReceived High priority call from Erika"
            ,"There are currently no teams available to respond to calls"
        };
        var pds = new PoliceDispatchService();
        pds.ReceiveCall("Alice", Priority.Low);
        pds.ReceiveCall("Billy", Priority.Low);
        pds.ReceiveCall("Casey", Priority.Low);
        pds.ReceiveCall("Devin", Priority.High);

        var result = pds.ReceiveCall("Erika", Priority.High);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }
    
    [TestMethod()]
    public void ReceiveCall_QueuedCalls_EmptyTeam()
    {
        var expected = new List<string> {
            "\nReceived Medium priority call from Frank"
            ,"There are currently no teams available to respond to calls"
        };
        var pds = new PoliceDispatchService();
        pds.ReceiveCall("Alice", Priority.High);
        pds.ReceiveCall("Billy", Priority.Low);
        pds.ReceiveCall("Casey", Priority.Medium);
        pds.ReceiveCall("Devin", Priority.High);
        pds.ReceiveCall("Erika", Priority.Low);
        
        var result = pds.ReceiveCall("Frank", Priority.Medium);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }
    
    [TestMethod()]
    public void ReturnTeamToStation_NoQueuedCalls_TeamReturns_AlreadyAtStation()
    {
        var expected = new List<string> {
            "\nTeam 2 is already at the station"
        };
        var pds = new PoliceDispatchService();

        var result = pds.ReturnTeamToStation(2);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }

    [TestMethod()]
    public void ReturnTeamToStation_NoQueuedCalls_TeamReturns()
    {
        var expected = new List<string> {
            "\nTeam 1 has returned from a Medium priority call"
            ,"There are no more calls to respond to"
        };
        var pds = new PoliceDispatchService();
        pds.ReceiveCall("Alice", Priority.Medium);

        var result = pds.ReturnTeamToStation(1);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }

    [TestMethod()]
    public void ReturnTeamToStation_QueuedCalls_TeamReturns_OneTeamHighPriority()
    {
        var expected = new List<string> {
            "\nTeam 4 has returned from a High priority call"
            ,"Team 4 has been dispatched to respond to a High priority call from Glenn"
            ,"There are currently no teams available to respond to calls"
        };
        var pds = new PoliceDispatchService();
        pds.ReceiveCall("Alice", Priority.High);
        pds.ReceiveCall("Billy", Priority.Low);
        pds.ReceiveCall("Casey", Priority.Medium);
        pds.ReceiveCall("Devin", Priority.High);
        pds.ReceiveCall("Erika", Priority.Low);
        pds.ReceiveCall("Frank", Priority.Medium);
        pds.ReceiveCall("Glenn", Priority.High);

        var result = pds.ReturnTeamToStation(4);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }


    [TestMethod()]
    public void ReturnTeamToStation_QueuedCalls_TeamReturns_OneTeamNoHighPriority()
    {
        var expected = new List<string> {
            "\nTeam 2 has returned from a Low priority call"
            ,"There are no high priority calls and only 1 team at the station, so nobody has been dispatched"
        };
        var pds = new PoliceDispatchService();
        pds.ReceiveCall("Alice", Priority.High);
        pds.ReceiveCall("Billy", Priority.Low);
        pds.ReceiveCall("Casey", Priority.Medium);
        pds.ReceiveCall("Devin", Priority.High);
        pds.ReceiveCall("Erika", Priority.Low);
        pds.ReceiveCall("Frank", Priority.Medium);
        pds.ReceiveCall("Glenn", Priority.High);
        pds.ReturnTeamToStation(4);

        var result = pds.ReturnTeamToStation(2);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }

    [TestMethod()]
    public void ReturnTeamToStation_QueuedCalls_TeamReturns_AvailableTeam()
    {
        var expected = new List<string> {
            "\nTeam 1 has returned from a High priority call"
            ,"Team 3 has been dispatched to respond to a Medium priority call from Inara"
            ,"There are no high priority calls and only 1 team at the station, so nobody has been dispatched"
        };
        var pds = new PoliceDispatchService();
        pds.ReceiveCall("Alice", Priority.High);
        pds.ReceiveCall("Billy", Priority.Low);
        pds.ReceiveCall("Casey", Priority.Medium);
        pds.ReceiveCall("Devin", Priority.High);
        pds.ReceiveCall("Erika", Priority.Low);
        pds.ReceiveCall("Frank", Priority.Medium);
        pds.ReceiveCall("Glenn", Priority.High);
        pds.ReturnTeamToStation(3);
        pds.ReturnTeamToStation(2);
        pds.ReturnTeamToStation(4);
        pds.ReturnTeamToStation(3);
        pds.ReceiveCall("Haley", Priority.Low);
        pds.ReceiveCall("Inara", Priority.Medium);

        var result = pds.ReturnTeamToStation(1);

        Assert.AreEqual(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i]);
        }
    }
}
