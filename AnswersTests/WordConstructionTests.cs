using Answers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AnswersTests;

[TestClass]
public class WordConstructionTests
{   
    [DynamicData(nameof(WordConstructionData), DynamicDataSourceType.Method)]
    [TestMethod()]
    public void WordConstructionTest(string[] words, string input, string[] expected, int scenario)
    {
        var result = WordConstruction.FindWordConstructions(words, input);

        Assert.AreEqual(expected.Length, result.Length, $"Scenario {scenario} failed");
        
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], result[i], $"Scenario {scenario} failed");
        }
    }

    private static IEnumerable<object[]> WordConstructionData()
    {
        return new List<object[]>()
        {
            new object[] {
                 new string [] { "good", "bad", "dog", "cat", "do", "dont" }
                ,"ddelgoo"
                ,new string[] { "good", "dog", "do" }
                ,1
            }
            ,new object[] {
                 new string [] { "jabberwocky", "alice", "rabbit", "hatter", "hare", "heart" }
                ,"theatre"
                ,new string[] { "hatter", "hare", "heart" }
                ,2
            }
            ,new object[] {
                 new string [] {}
                ,"empty"
                ,new string[] {}
                ,3
            }
            ,new object[] {
                 new string [] {}
                ,""
                ,new string[] {}
                ,4
            }
            ,new object[] {
                 new string [] { "empty" , "results"}
                ,""
                ,new string[] {}
                ,5
            }
            ,new object[] {
                 new string [] { "none", "null", "nonce", "nill", "negative", "zilch" }
                ,"unconcentrationally"
                ,new string[] { "none", "null", "nonce", "nill" }
                ,6
            }
            ,new object[] {
                 new string [] { "none", "null", "nonce", "nill", "negative", "zilch" }
                ,"rabbit"
                ,new string[] {}
                ,7
            }
        };
    }
}